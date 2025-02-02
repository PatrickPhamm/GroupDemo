﻿using KoiShowManagementSystem.Data.Models;
using KoiShowManagementSystem.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShowManagementSystem.Data
{
    public class UnitOfWork
    {
        private FA24_SE1717_PRN231_G2_KoiShowManagementSystemContext context;
        private ResultRepository _resultRepository;
        private JudgeRepository _judgeRepository;
        private JudgesCriteriaRepository judgesCriteriarepository;
        private ContestRepository _contestRepository;
        private KoiRepository _koiRepository;
        private ApplicationRepository _applicationRepository;

        public UnitOfWork() {
            context ??= new FA24_SE1717_PRN231_G2_KoiShowManagementSystemContext();
        }

        public ResultRepository resultRepository
        {
            get { return _resultRepository ??= new ResultRepository(context); }
        }

        public JudgeRepository judgeRepository
        {
            get { return _judgeRepository ??= new JudgeRepository(context); }
        }

        public JudgesCriteriaRepository JudgesCriteriaRepo
        {
            get { return judgesCriteriarepository ??= new JudgesCriteriaRepository(context); }
        }
        public ContestRepository contestRepository
        {
            get { return _contestRepository ??= new ContestRepository(context); }
        }
        public KoiRepository koiRepository
        {
            get { return _koiRepository ??= new KoiRepository(context); }
        }
        public ApplicationRepository applicationRepository
        {
            get { return _applicationRepository ??= new ApplicationRepository(context); }
        }

        ////TO-DO CODE HERE/////////////////

        #region Set transaction isolation levels

        /*
        Read Uncommitted: The lowest level of isolation, allows transactions to read uncommitted data from other transactions. This can lead to dirty reads and other issues.

        Read Committed: Transactions can only read data that has been committed by other transactions. This level avoids dirty reads but can still experience other isolation problems.

        Repeatable Read: Transactions can only read data that was committed before their execution, and all reads are repeatable. This prevents dirty reads and non-repeatable reads, but may still experience phantom reads.

        Serializable: The highest level of isolation, ensuring that transactions are completely isolated from one another. This can lead to increased lock contention, potentially hurting performance.

        Snapshot: This isolation level uses row versioning to avoid locks, providing consistency without impeding concurrency. 
         */

        public int SaveChangesWithTransaction()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    result = context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    result = await context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }
        #endregion
    }
}
