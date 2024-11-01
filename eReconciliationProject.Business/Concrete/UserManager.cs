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
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.Entities.Dtos;

namespace eReconciliationProject.Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserRepository _userRepository;
        private IUserOperationClaimService _userOperationClaimService;
        private IUserCompanyService _userCompanyService;

        //ProjectContext context = new();
        private readonly UserOperationClaimManager _userOperation;
        public UserManager(IUserRepository userRepository, IUserOperationClaimService userOperationClaimService, IUserCompanyService userCompanyService)
        {
            _userRepository = userRepository;
            _userOperationClaimService = userOperationClaimService;
            _userCompanyService = userCompanyService;
        }


        [CacheRemoveAspect("IUserService.Get")]
        [ValidationAspect(typeof(UserValidator))]
        public void Add(User user)
        {
            _userRepository.Add(user);
            //context.Users.Add(user);
        }


        [CacheAspect(60)]
        public User GetById(int id)
        {
            //var result = context.Users.FirstOrDefault(u => u.Id == id);
            return _userRepository.Get(u => u.Id == id);

        }
        public IDataResult<User> Get(int id)
        {

            var result = _userRepository.Get(u => u.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<User>(result);
            }
            return new SuccessDataResult<User>();

        }

        [CacheAspect(60)]
        public User GetByMail(string email)
        {
            return _userRepository.Get(u => u.Email == email);

        }

        [CacheAspect(60)]
        public User GetByMailConfirmValue(string value)
        {
            return _userRepository.Get(u => u.MailConfirmValue == value);

        }

        public List<OperationClaim> GetClaims(User user, int companyId)
        {
            //var result = from operationClaim in context.OperationClaims
            //             join userOperationClaim in context.UserOperationClaims on operationClaim.Id equals userOperationClaim.OperationClaimId
            //             where userOperationClaim.CompanyId == companyId && userOperationClaim.UserId == user.Id
            //             select new OperationClaim
            //             {
            //                 Id = operationClaim.Id,
            //                 Name = operationClaim.Name,
            //             };
            //return result.ToList();

            return _userRepository.GetClaims(user, companyId);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("User.GetList,Admin")]
        public IDataResult<List<UserCompanyForListDto>> GetListUserDto(int companyId)
        {
            //var result = (from userCompany in context.UserCompanies.Where(x => x.CompanyId == companyId && x.IsActive == true)
            //              join user in context.Users on userCompany.UserId equals user.Id
            //              where user.IsActive == true
            //              select new UserCompanyForListDto
            //              {
            //                  Id = userCompany.Id,
            //                  UserId = userCompany.UserId,
            //                  CompanyId = companyId,
            //                  Email = user.Email,
            //                  Name = user.Name,
            //                  UserAddedAt = user.AddedAt,
            //                  UserIsActive = user.IsActive,
            //                  UserMailValue = user.MailConfirmValue
            //              }).OrderBy(x => x.Name).ToList();

            return new SuccessDataResult<List<UserCompanyForListDto>>(_userRepository.GetUserListDto(companyId));


        }

        [PerformanceAspect(3)]
        //[SecuredOperation("User.Update,Admin")]
        [CacheRemoveAspect("IUserService.Update")]
        public void Update(User user)
        {
            _userRepository.Update(user);
            //context.Users.Update(user);
            //context.SaveChanges();
        }

        [CacheRemoveAspect("IUserService.Update")]
        public IResult UpdateResult(UserForRegisterToSecondAccountDto userForRegister)
        {
            var findUser = _userRepository.Get(x => x.Id == userForRegister.Id);
            findUser.Name = userForRegister.Name;
            findUser.Email = userForRegister.Email;

            if (userForRegister.Password != "")
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(userForRegister.Password, out passwordHash, out passwordSalt);
                findUser.PasswordHash = passwordHash;
                findUser.PasswordSalt = passwordSalt;
            }

            //context.Users.Update(findUser);
            //context.SaveChanges();

            _userRepository.Update(findUser);
            return new SuccessResult(Messages.UpdateUser);
        }

        public IDataResult<List<OperationClaimForUserListDto>> GetOperationClaimForUserList(string value, int companyId)
        {
            //var user = context.Users.Where(p => p.MailConfirmValue == value).FirstOrDefault();

            //var result = (from operationClaim in context.OperationClaims
            //              where operationClaim.Name != "Admin" && !operationClaim.Name.Contains("UserOperationClaim")
            //              select new OperationClaimForUserListDto
            //              {
            //                  Id = operationClaim.Id,
            //                  Name = operationClaim.Name,
            //                  Description = operationClaim.Description,
            //                  Status = (context.UserOperationClaims.Where(p => p.UserId == user.Id && p.OperationClaimId == operationClaim.Id && p.CompanyId == companyId).Count() > 0 ? true : false),
            //                  UserName = user.Name,
            //                  UserId = user.Id,
            //                  CompanyId = companyId
            //              }).OrderBy(x => x.Name).ToList();

            return new SuccessDataResult<List<OperationClaimForUserListDto>>(_userRepository.GetOperationClaimForUserList(value, companyId));
        }

        public IResult UpdateOperationClaim(OperationClaimForUserListDto operationClaim)
        {
            if (operationClaim.Status == true)
            {
                //var query = context.UserOperationClaims.Where(x => x.UserId == operationClaim.UserId && x.CompanyId == operationClaim.CompanyId).ToList().FirstOrDefault(x => x.OperationClaimId == operationClaim.Id);
                var result = _userOperationClaimService.GetList(operationClaim.UserId, operationClaim.CompanyId).Data.Where(p => p.OperationClaimId == operationClaim.Id).FirstOrDefault();
                _userOperationClaimService.Delete(result);
                //context.UserOperationClaims.Remove(query);
                //context.SaveChanges();
            }
            else
            {
                UserOperationClaim userOperationClaim = new UserOperationClaim()
                {
                    CompanyId = operationClaim.CompanyId,
                    AddedAt = DateTime.Now,
                    IsActive = true,
                    OperationClaimId = operationClaim.Id,
                    UserId = operationClaim.UserId
                };
                //context.UserOperationClaims.Add(userOperationClaim);
                //context.SaveChanges();

                _userOperationClaimService.Add(userOperationClaim);
            }

            return new SuccessResult(Messages.UpdatedUserOperationClaim);
        }

        public IResult UserCompanyDelete(int userId, int companyId)
        {
            var userOperationClaims = _userOperationClaimService.GetList(userId, companyId).Data;
            foreach (var userOperationClaim in userOperationClaims)
            {
                _userOperationClaimService.Delete(userOperationClaim);
            }

            var result = _userCompanyService.GetByUserIdAndCompanyId(userId, companyId);

            _userCompanyService.Delete(result);

            return new SuccessResult(Messages.DeletedUserCompanyRelationship);
        }

        public IDataResult<List<AdminCompaniesForUserDto>> GetAdminCompaniesForUser(int userId, int companyId)
        {
            return new SuccessDataResult<List<AdminCompaniesForUserDto>>(_userRepository.GetAdminCompaniesForUser(userId, companyId));
        }
    }
}
