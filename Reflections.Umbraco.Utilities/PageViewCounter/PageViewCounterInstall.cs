﻿using NPoco;
using Semver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Logging;
using Umbraco.Core.Migrations;
using Umbraco.Core.Migrations.Upgrade;
using Umbraco.Core.Scoping;
using Umbraco.Core.Services;

namespace Reflections.UmbracoUtilities
{
    public class PageViewCounterComposer : ComponentComposer<PageViewCounterComponent>, IUserComposer
    {
        public override void Compose(Composition composition)
        {
            // ApplicationStarting event in V7: add IContentFinders, register custom services and more here

            base.Compose(composition);
        }
    }

    public class PageViewCounterComponent : IComponent
    {
        private readonly IScopeProvider scopeProvider;
        private readonly IMigrationBuilder migrationBuilder;
        private readonly IKeyValueService keyValueService;
        private readonly ILogger logger;

        public PageViewCounterComponent(
       IScopeProvider scopeProvider,
       IMigrationBuilder migrationBuilder,
       IKeyValueService keyValueService,
       ILogger logger)
        {
            this.scopeProvider = scopeProvider;
            this.migrationBuilder = migrationBuilder;
            this.keyValueService = keyValueService;
            this.logger = logger;
        }

        public void Initialize()
        {
            // ApplicationStarted event in V7: add your events here

            var plan = new MigrationPlan("PageViewCounter");
            plan.From(string.Empty)
                .To<PageViewCounterMigration>("Initialize");

            var upgrader = new Upgrader(plan);
            upgrader.Execute(scopeProvider, migrationBuilder, keyValueService, logger);

        }

        public void Terminate()
        { }
    }

    public class PageViewCounterMigration : MigrationBase
    {
        public PageViewCounterMigration(IMigrationContext context) : base(context)
        { }

        public override void Migrate()
        {
            if (!this.TableExists("ReflectionsUmbracoUtilitiesPageViewCounter"))
            {
                this.Create.Table<ReflectionsUmbracoUtilitiesPageViewCounter>().Do();
            }

            StringBuilder sb = new StringBuilder(string.Empty);

            sb.AppendLine("IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReflectionsUmbracoUtilitiesSetPageViewCount]') AND type in (N'P', N'PC'))");
            sb.AppendLine("BEGIN");
            sb.AppendLine("execute ('");
            sb.AppendLine("CREATE PROCEDURE [dbo].[ReflectionsUmbracoUtilitiesSetPageViewCount]");
            sb.AppendLine("(");
            sb.AppendLine("@@NodeId INT");
            sb.AppendLine(")");
            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");
            sb.AppendLine("SET NOCOUNT ON;");
            sb.AppendLine("IF EXISTS(SELECT 1 FROM ReflectionsUmbracoUtilitiesPageViewCounter WHERE NodeId = @@NodeId)");
            sb.AppendLine("BEGIN");
            sb.AppendLine("UPDATE ReflectionsUmbracoUtilitiesPageViewCounter SET ViewsCounter = ViewsCounter + 1");
            sb.AppendLine("WHERE NodeId = @@NodeId");
            sb.AppendLine("END");
            sb.AppendLine("ELSE");
            sb.AppendLine("BEGIN");
            sb.AppendLine("INSERT INTO ReflectionsUmbracoUtilitiesPageViewCounter(NodeId, ViewsCounter) VALUES (@@NodeId, 1)");
            sb.AppendLine("END");
            sb.AppendLine("END");
            sb.AppendLine("')");
            sb.AppendLine("END");

            var SQLStatment = new Sql(sb.ToString());
            Database.Execute(SQLStatment);


            sb.Clear();

            sb.AppendLine("IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReflectionsUmbracoUtilitiesGetPageViewCount]') AND type in (N'P', N'PC'))");
            sb.AppendLine("BEGIN");
            sb.AppendLine("execute ('");
            sb.AppendLine("CREATE PROCEDURE [dbo].[ReflectionsUmbracoUtilitiesGetPageViewCount]");
            sb.AppendLine("(");
            sb.AppendLine("@@NodeId INT");
            sb.AppendLine(")");
            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");
            sb.AppendLine("");
            sb.AppendLine("SET NOCOUNT ON;");
            sb.AppendLine("");
            sb.AppendLine("SELECT ISNULL(ViewsCounter,0)");
            sb.AppendLine("FROM ReflectionsUmbracoUtilitiesPageViewCounter");
            sb.AppendLine("WHERE NodeId = @@NodeId");
            sb.AppendLine("");
            sb.AppendLine("END");
            sb.AppendLine("')");
            sb.AppendLine("END");

            SQLStatment = new Sql(sb.ToString());

            Database.Execute(SQLStatment);
        }
    }

    [TableName("ReflectionsUmbracoUtilitiesPageViewCounter")]
    [PrimaryKey("NodeId", AutoIncrement = false)]
    public class ReflectionsUmbracoUtilitiesPageViewCounter
    {
        [Column("NodeId")]
        public int NodeId { get; set; }

        [Column("ViewsCounter")]
        public int ViewsCounter { get; set; }
    }
}
