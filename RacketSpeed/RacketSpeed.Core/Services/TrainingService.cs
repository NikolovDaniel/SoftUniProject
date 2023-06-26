﻿using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RacketSpeed.Core.Contracts;
using RacketSpeed.Core.Models.Training;
using RacketSpeed.Infrastructure.Data.Entities;
using RacketSpeed.Infrastructure.Data.Repositories;

namespace RacketSpeed.Core.Services
{
    /// <summary>
    /// Service class for Training Entity.
    /// </summary>
    public class TrainingService : ITrainingService
    {
        /// <summary>
        /// Repository.
        /// </summary>
        protected IRepository repository;

        /// <summary>
        /// DI repository.
        /// </summary>
        /// <param name="repository">Repository.</param>
		public TrainingService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task AddAsync(TrainingFormModel model)
        {
            var training = new Training()
            {
                Name = model.Name,
                Start = model.Start,
                End = model.End,
                CoachId = model.CoachId,
                DayOfWeek = model.DayOfWeek,
                IsDeleted = false
            };

            await this.repository.AddAsync<Training>(training);
            await this.repository.SaveChangesAsync();
        }

        public async Task<ICollection<TrainingViewModel>> AllAsync(Guid coachId)
        {
            Expression<Func<Training, bool>> expression
                = p => p.CoachId == coachId && p.IsDeleted == false;

            var coachTrainings = this.repository.All<Training>(expression);

            return await coachTrainings
                            .Select(ct => new TrainingViewModel()
                            {
                            })
                            .ToListAsync();
        }

        public async Task DeleteAsync(Guid trainingId)
        {
            var training = await this.repository.GetByIdAsync<Training>(trainingId);

            if (training == null)
            {
                return;
            }

            training.IsDeleted = true;

            await this.repository.SaveChangesAsync();
        }

        public async Task EditAsync(TrainingFormModel model)
        {
            var training = await this.repository.GetByIdAsync<Training>(model.Id);

            if (training == null)
            {
                return;
            }

            training.Id = model.Id;
            training.CoachId = model.CoachId;
            training.Name = model.Name;
            training.Start = model.Start;
            training.End = model.End;
            training.DayOfWeek = model.DayOfWeek;
            training.CoachId = model.CoachId;

            await this.repository.SaveChangesAsync();
        }

        public async Task<TrainingFormModel> GetByIdAsync(Guid trainingId)
        {
            var training = await this.repository.GetByIdAsync<Training>(trainingId);

            if (training == null)
            {
                return null;
            }

            var model = new TrainingFormModel()
            {
                Id = training.Id,
                Name = training.Name,
                Start = training.Start,
                End = training.End,
                DayOfWeek = training.DayOfWeek,
                CoachId = training.CoachId
            };

            return model;
        }
    }
}

