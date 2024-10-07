using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Business.BusinessAspect;
using eReconciliationProject.Business.Constans;
using eReconciliationProject.Business.ValidationRules.FluentValidation;
using eReconciliationProject.Core.Aspects.Autofac.Validation;
using eReconciliationProject.Core.Aspects.Caching;
using eReconciliationProject.Core.Aspects.Performance;
using eReconciliationProject.Core.Concrete;
using eReconciliationProject.Core.Utilities.Hashing;
using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Core.Utilities.Results.Concrete;
using eReconciliationProject.DA.Context;
using eReconciliationProject.Entities.Dtos;

namespace eReconciliationProject.Business.Concrete
{
    public class UserManager : IUserService
    {
        //private IUserRepository _userRepository;

        ProjectContext context = new();

        //public UserManager(IUserRepository userRepository)
        //{
        //    _userRepository = userRepository;
        //}


        [CacheRemoveAspect("IUserService.Get")]
        [ValidationAspect(typeof(UserValidator))]
        public void Add(User user)
        {

            context.Users.Add(user);
        }


        [CacheAspect(60)]
        public User GetById(int id)
        {
            var result = context.Users.FirstOrDefault(u => u.Id == id);
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public IDataResult<User> Get(int id)
        {

            var result = context.Users.FirstOrDefault(u => u.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<User>(result);
            }
            return new SuccessDataResult<User>();

        }

        [CacheAspect(60)]
        public User GetByMail(string email)
        {
            var result = context.Users.FirstOrDefault(u => u.Email == email);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        [CacheAspect(60)]
        public User GetByMailConfirmValue(string value)
        {
            var result = context.Users.FirstOrDefault(u => u.MailConfirmValue == value);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public List<OperationClaim> GetClaims(User user, int companyId)
        {
            var result = from operationClaim in context.OperationClaims
                         join userOperationClaim in context.UserOperationClaims on operationClaim.Id equals userOperationClaim.OperationClaimId
                         where userOperationClaim.CompanyId == companyId && userOperationClaim.UserId == user.Id
                         select new OperationClaim
                         {
                             Id = operationClaim.Id,
                             Name = operationClaim.Name,
                         };
            return result.ToList();

            //return _userRepository.GetClaims(user, companyId);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("User.GetList,Admin")]
        public IDataResult<List<UserCompanyForListDto>> GetListUserDto(int companyId)
        {
            var result = (from userCompany in context.UserCompanies.Where(x => x.CompanyId == companyId && x.IsActive == true)
                          join user in context.Users on userCompany.UserId equals user.Id
                          select new UserCompanyForListDto
                          {
                              Id = userCompany.Id,
                              UserId = userCompany.UserId,
                              CompanyId = companyId,
                              Email = user.Email,
                              Name = user.Name,
                              UserAddedAt = user.AddedAt,
                              UserIsActive = user.IsActive
                          }).OrderBy(x => x.Name).ToList();

            return new SuccessDataResult<List<UserCompanyForListDto>>(result);


        }

        [PerformanceAspect(3)]
        //[SecuredOperation("User.Update,Admin")]
        [CacheRemoveAspect("IUserService.Update")]
        public void Update(User user)
        {
            context.Users.Update(user);
            context.SaveChanges();
        }

        [CacheRemoveAspect("IUserService.Update")]
        public IResult UpdateResult(UserForRegisterToSecondAccountDto userForRegister)
        {
            var findUser = context.Users.FirstOrDefault(x => x.Id == userForRegister.Id);
            findUser.Name = userForRegister.Name;
            findUser.Email = userForRegister.Email;

            if (userForRegister.Password != "")
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(userForRegister.Password, out passwordHash, out passwordSalt);
                findUser.PasswordHash = passwordHash;
                findUser.PasswordSalt = passwordSalt;
            }

            context.Users.Update(findUser);
            context.SaveChanges();

            return new SuccessResult(Messages.UpdateUser);
        }
    }
}
