using System;
using System.Data;
using Dapper;
using MyStore.Core.Domain;

namespace MyStore.Infrastructure.Dapper.Configurations
{
    public class AggregateIdConfiguration : SqlMapper.TypeHandler<AggregateId>
    {
        public override void SetValue(IDbDataParameter parameter, AggregateId value)
        {
            parameter.Value = value;
        }

        public override AggregateId Parse(object value)
            => new AggregateId((Guid) value);
    }
}