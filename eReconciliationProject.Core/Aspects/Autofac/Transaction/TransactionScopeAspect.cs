﻿using Castle.DynamicProxy;
using eReconciliationProject.Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace eReconciliationProject.Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect:MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using(TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();
                    transactionScope.Complete();
                }
                catch (Exception)
                {

                    transactionScope.Dispose();
                    throw;
                }
            }

            
        }
    }
}
