using Rainbow.Common;
using Rainbow.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Yunyong.Core;
using Yunyong.Core.ViewModels;

namespace Rainbow.Services
{
    internal static class OptionTrackExtensions
    {
        public static CustomerOperationTrackRecord CustomerOperationTrack<TEntity>(
                this CreateVM vm,
                Guid customerId,
                Guid recordId
            )
            where TEntity : Entity
        {
            var entityType = typeof(TEntity);
            var entityDisplayName = entityType.GetCustomAttribute<DisplayAttribute>()?.Name ?? entityType.Name;

            var optionTrack = EntityFactory.Create<CustomerOperationTrackRecord>();
            optionTrack.CustomerId = customerId;

            optionTrack.RecordId = recordId;
            optionTrack.TargetEntity = entityType.Name;
            optionTrack.Option = "Create";
            optionTrack.OptionComments = $"创建{entityDisplayName}";

            return optionTrack;
        }

        public static CustomerOperationTrackRecord CustomerOperationTrack<TEntity>(this UpdateVM vm, Guid customerId)
            where TEntity : Entity
        {
            var optionTrack = EntityFactory.Create<CustomerOperationTrackRecord>();
            optionTrack.CustomerId = customerId;
            optionTrack.UpdateTrack<TEntity, UpdateVM>(vm);

            return optionTrack;
        }

        public static CustomerOperationTrackRecord CustomerOperationTrack<TEntity>(this DeleteVM vm, Guid customerId)
            where TEntity : Entity
        {
            var optionTrack = EntityFactory.Create<CustomerOperationTrackRecord>();
            optionTrack.CustomerId = customerId;
            optionTrack.UpdateTrack<TEntity, DeleteVM>(vm);

            return optionTrack;
        }

        public static CustomerServiceOperationTrackRecord CustomerServiceOperationTrack<TEntity>(
                this CreateVM vm,
                Guid customerServiceId,
                Guid recordId
            )
            where TEntity : Entity
        {
            var entityType = typeof(TEntity);
            var entityDisplayName = entityType.GetCustomAttribute<DisplayAttribute>()?.Name ?? entityType.Name;

            var optionTrack = EntityFactory.Create<CustomerServiceOperationTrackRecord>();
            optionTrack.CustomerServiceId = customerServiceId;

            optionTrack.RecordId = recordId;
            optionTrack.TargetEntity = entityType.Name;
            optionTrack.Option = "Create";
            optionTrack.OptionComments = $"创建{entityDisplayName}";

            return optionTrack;
        }

        public static CustomerServiceOperationTrackRecord CustomerServiceOperationTrack<TEntity>(
                this UpdateVM vm,
                Guid customerServiceId
            )
            where TEntity : Entity
        {
            var optionTrack = EntityFactory.Create<CustomerServiceOperationTrackRecord>();
            optionTrack.CustomerServiceId = customerServiceId;
            optionTrack.UpdateTrack<TEntity, UpdateVM>(vm);

            return optionTrack;
        }

        public static CustomerServiceOperationTrackRecord CustomerServiceOperationTrack<TEntity>(
                this DeleteVM vm,
                Guid customerServiceId
            )
            where TEntity : Entity
        {
            var optionTrack = EntityFactory.Create<CustomerServiceOperationTrackRecord>();
            optionTrack.CustomerServiceId = customerServiceId;
            optionTrack.UpdateTrack<TEntity, DeleteVM>(vm);

            return optionTrack;
        }

        private static void UpdateTrack<TEntity, TVM>(this IOperationTrackRecord target, TVM vm)
            where TEntity : Entity where TVM : VMBase
        {
            var entityType = typeof(TEntity);
            target.TargetEntity = entityType.Name;
            var entityDisplayName = entityType.GetCustomAttribute<DisplayAttribute>()?.Name ?? entityType.Name;

            if (vm is UpdateVM updateVM)
            {
                target.Option = "Update";
                target.RecordId = updateVM.Id;
                target.OptionComments = $"更新{entityDisplayName}";
            }

            if (vm is DeleteVM deleteVM)
            {
                target.RecordId = deleteVM.Id;
                target.Option = "Delete";
                target.OptionComments = $"删除{entityDisplayName}";
            }
        }
    }
}