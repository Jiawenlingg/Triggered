using Microsoft.AspNetCore.Identity;
using triggeredapi.Models;

namespace triggeredapi.Repo
{
    public interface IUnitOfWork
    {
        UserManager<User> Users { get; }
        IRepository<Novel> Novels { get; }
        /// <summary>
        /// Commits all changes
        /// </summary>
        void Commit();
        /// <summary>
        /// Discards all changes that has not been commited
        /// </summary>
        void RejectChanges();
        void Dispose();
        }
}