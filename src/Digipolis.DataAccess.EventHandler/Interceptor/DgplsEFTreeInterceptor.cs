//using System;
//using System.Collections.Generic;
//using System.Data.Common;
//using System.Data.Entity.Core.Common.CommandTrees;
//using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
//using System.Data.Entity.Core.Metadata.Edm;
//using System.Data.Entity.Infrastructure.Interception;
//using System.Linq;
//using System.Threading.Tasks;
//using Digipolis.Eventhandler;

//namespace Digipolis.DataAccess.EventHandler
//{
//    //public class DgplsEFInterceptor : IDbCommandInterceptor
//    public class DgplsEFTreeInterceptor : IDbCommandTreeInterceptor

//    {
//        public IEventHandler MyEventHandler { get; set; }
//        public DgplsEFTreeInterceptor(IEventHandler eventHandler)
//        {
//            MyEventHandler = eventHandler;

//        }

//        public void TreeCreated(DbCommandTreeInterceptionContext interceptionContext)
//        {
//            if (interceptionContext.OriginalResult.DataSpace != DataSpace.SSpace)
//            {
//                return;
//            }

//            var queryCommand = interceptionContext.Result as DbQueryCommandTree;
//            if (queryCommand != null)
//            {
//                interceptionContext.Result = HandleQueryCommand(queryCommand);
//            }

//            var deleteCommand = interceptionContext.OriginalResult as DbDeleteCommandTree;
//            if (deleteCommand != null)
//            {
//                var left = 0;
//                var right = ((DbConstantExpression)(((DbBinaryExpression)(deleteCommand.Predicate)).Right)).Value;


//                interceptionContext.Result = HandleDeleteCommand(deleteCommand);
//            }

//            var insertCommand = interceptionContext.Result as DbInsertCommandTree;
//            if (insertCommand != null)
//            {                
//                interceptionContext.Result = HandleInsertCommand(insertCommand);
//            }

//            var updateCommand = interceptionContext.OriginalResult as DbUpdateCommandTree;
//            if (updateCommand != null)
//            {
//                interceptionContext.Result = HandleUpdateCommand(updateCommand);
//            }
//        }



//        private static DbCommandTree HandleDeleteCommand(DbDeleteCommandTree deleteCommand)
//        {
//            var setClauses = new List<DbModificationClause>();
//            var table = (EntityType)deleteCommand.Target.VariableType.EdmType;

//            //if (table.Properties.All(p => p.Name != IsDeletedColumnName))
//            //{
//                return deleteCommand;
//            //}

//            //setClauses.Add(DbExpressionBuilder.SetClause(
//            //    deleteCommand.Target.VariableType.Variable(deleteCommand.Target.VariableName).Property(IsDeletedColumnName),
//            //    DbExpression.FromBoolean(true)));

//            //return new DbUpdateCommandTree(
//            //    deleteCommand.MetadataWorkspace,
//            //    deleteCommand.DataSpace,
//            //    deleteCommand.Target,
//            //    deleteCommand.Predicate,
//            //    setClauses.AsReadOnly(), null);
//        }


//        private static DbCommandTree HandleQueryCommand(DbQueryCommandTree queryCommand)
//        {
//            var newQuery = queryCommand.Query.Accept(new SoftDeleteQueryVisitor());
//            return new DbQueryCommandTree(
//                queryCommand.MetadataWorkspace,
//                queryCommand.DataSpace,
//                newQuery);
//        }

//        public class SoftDeleteQueryVisitor : DefaultExpressionVisitor
//        {
//            public override DbExpression Visit(DbScanExpression expression)
//            {
//                var table = (EntityType)expression.Target.ElementType;
//                //if (table.Properties.All(p => p.Name != IsDeletedColumnName))
//                //{
//                return base.Visit(expression);
//                //}

//                var binding = expression.Bind();
//                //    return binding.Filter(
//                //        binding.VariableType
//                //            .Variable(binding.VariableName)
//                //            .Property(IsDeletedColumnName)
//                //            .NotEqual(DbExpression.FromBoolean(true)));
//                //}
//            }

//        }

//            private static DbCommandTree HandleInsertCommand(DbInsertCommandTree insertCommand)
//            {
//                var now = DateTime.Now;
//            var Tabelnaam = insertCommand.Target.VariableType.EdmType.Name;
//            var table = (EntityType)insertCommand.Target.VariableType.EdmType;

            

//            var setClauses = insertCommand.SetClauses
//                    //.Select(clause => clause.UpdateIfMatch(CreatedColumnName, DbExpression.FromDateTime(now)))
//                    //.Select(clause => clause.UpdateIfMatch(ModifiedColumnName, DbExpression.FromDateTime(now)))
//                    .ToList();

//                return new DbInsertCommandTree(
//                    insertCommand.MetadataWorkspace,
//                    insertCommand.DataSpace,
//                    insertCommand.Target,
//                    setClauses.AsReadOnly(),
//                    insertCommand.Returning);
//            }

//            private static DbCommandTree HandleUpdateCommand(DbUpdateCommandTree updateCommand)
//            {
//                var now = DateTime.Now;

//                var setClauses = updateCommand.SetClauses
//                   // .Select(clause => clause.UpdateIfMatch(ModifiedColumnName, DbExpression.FromDateTime(now)))
//                    .ToList();

//                return new DbUpdateCommandTree(
//                    updateCommand.MetadataWorkspace,
//                    updateCommand.DataSpace,
//                    updateCommand.Target,
//                    updateCommand.Predicate,
//                    setClauses.AsReadOnly(), null);
//            }



       
//    }
//}