﻿using Microsoft.EntityFrameworkCore;
using RacketSpeed.Core.Contracts;
using RacketSpeed.Core.Models.Post;
using RacketSpeed.Infrastructure.Data.Entities;
using RacketSpeed.Infrastructure.Data.Repositories;

namespace RacketSpeed.Core.Services
{
    /// <summary>
    /// Service class for Post Entity.
    /// </summary>
    public class PostService : IPostService
	{
        /// <summary>
        /// Repository.
        /// </summary>
        protected IRepository repository;

        /// <summary>
        /// DI repository.
        /// </summary>
        /// <param name="repository"></param>
		public PostService(IRepository repository)
		{
            this.repository = repository;
		}

        /// <inheritdoc></inheritdoc>
        public async Task AddAsync(PostViewModel model)
        {
            var post = new Post()
            {
                Title = model.Title,
                Content = model.Content,
                IsDeleted = false
            };

            await this.repository.AddAsync<Post>(post);
            await this.repository.SaveChangesAsync();
        }

        /// <inheritdoc></inheritdoc>
        public async Task<ICollection<PostViewModel>> AllAsync()
        {
            var allPosts = this.repository.AllReadonly<Post>();

            return await allPosts
                .Select(p => new PostViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                   Content = p.Content
                })
                .ToListAsync();
        }

        /// <inheritdoc></inheritdoc>
        public async Task<PostViewModel> GetByIdAsync(Guid id)
        {
            var post = await this.repository.GetByIdAsync<Post>(id);

            if (post == null)
            {
                return null;
            }

            var model = new PostViewModel()
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content
            };

            return model;
        }

        /// <inheritdoc></inheritdoc>
        public async Task EditAsync(PostViewModel model)
        {
            var post = await this.repository.GetByIdAsync<Post>(model.Id);

            post.Title = model.Title;
            post.Content = model.Content;

            await this.repository.SaveChangesAsync();
        }

        /// <inheritdoc></inheritdoc>
        public async Task DeleteAsync(Guid id)
        {
            var post = await this.repository.GetByIdAsync<Post>(id);

            if (post == null)
            {
                throw new InvalidCastException(); // might need fix
            } 

            post.IsDeleted = true;

            await this.repository.SaveChangesAsync();
        }
    }
}

