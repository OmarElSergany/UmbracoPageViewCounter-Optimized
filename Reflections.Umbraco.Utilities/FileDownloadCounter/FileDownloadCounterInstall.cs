using NPoco;
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
    public class FileDownloadCounterComposer : ComponentComposer<FileDownloadCounterComponent>, IUserComposer
    {
        public override void Compose(Composition composition)
        {
            // ApplicationStarting event in V7: add IContentFinders, register custom services and more here

            base.Compose(composition);
        }
    }

    public class FileDownloadCounterComponent : IComponent
    {
        private readonly IScopeProvider scopeProvider;
        private readonly IMigrationBuilder migrationBuilder;
        private readonly IKeyValueService keyValueService;
        private readonly ILogger logger;

        public FileDownloadCounterComponent(
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

            var plan = new MigrationPlan("FileDownloadCounter");
            plan.From(string.Empty)
                .To<FileDownloadCounterMigration>("Initialize");

            var upgrader = new Upgrader(plan);
            upgrader.Execute(scopeProvider, migrationBuilder, keyValueService, logger);

        }

        public void Terminate()
        { }
    }

    public class FileDownloadCounterMigration : MigrationBase
    {
        public FileDownloadCounterMigration(IMigrationContext context) : base(context)
        { }

        public override void Migrate()
        {
            if (!this.TableExists("ReflectionsUmbracoUtilitiesFileDownloadCounter"))
            {
                this.Create.Table<ReflectionsUmbracoUtilitiesFileDownloadCounter>().Do();
            }

            StringBuilder sb = new StringBuilder(string.Empty);

            sb.AppendLine("IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReflectionsUmbracoUtilitiesSetFileDownloadCount]') AND type in (N'P', N'PC'))");
            sb.AppendLine("BEGIN");
            sb.AppendLine("execute ('");
            sb.AppendLine("CREATE PROCEDURE [dbo].[ReflectionsUmbracoUtilitiesSetFileDownloadCount]");
            sb.AppendLine("(");
            sb.AppendLine("@@NodeId INT,");
            sb.AppendLine("@@MediaId INT");
            sb.AppendLine(")");
            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");
            sb.AppendLine("SET NOCOUNT ON;");
            sb.AppendLine("IF EXISTS(SELECT 1 FROM ReflectionsUmbracoUtilitiesFileDownloadCounter WHERE NodeId = @@NodeId and MediaId = @@MediaId)");
            sb.AppendLine("BEGIN");
            sb.AppendLine("UPDATE ReflectionsUmbracoUtilitiesFileDownloadCounter SET DownloadCounter = DownloadCounter + 1");
            sb.AppendLine("WHERE NodeId = @@NodeId and MediaId = @@MediaId");
            sb.AppendLine("END");
            sb.AppendLine("ELSE");
            sb.AppendLine("BEGIN");
            sb.AppendLine("INSERT INTO ReflectionsUmbracoUtilitiesFileDownloadCounter(NodeId, MediaId, DownloadCounter) VALUES (@@NodeId, @@MediaId, 1)");
            sb.AppendLine("END");
            sb.AppendLine("END");
            sb.AppendLine("')");
            sb.AppendLine("END");

            var SQLStatment = new Sql(sb.ToString());
            Database.Execute(SQLStatment);


            sb.Clear();

            sb.AppendLine("IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReflectionsUmbracoUtilitiesGetFileDownloadCount]') AND type in (N'P', N'PC'))");
            sb.AppendLine("BEGIN");
            sb.AppendLine("execute ('");
            sb.AppendLine("CREATE PROCEDURE [dbo].[ReflectionsUmbracoUtilitiesGetFileDownloadCount]");
            sb.AppendLine("(");
            sb.AppendLine("@@NodeId INT,");
            sb.AppendLine("@@MediaId INT");
            sb.AppendLine(")");
            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");
            sb.AppendLine("");
            sb.AppendLine("SET NOCOUNT ON;");
            sb.AppendLine("");
            sb.AppendLine("SELECT ISNULL(DownloadCounter,0)");
            sb.AppendLine("FROM ReflectionsUmbracoUtilitiesFileDownloadCounter");
            sb.AppendLine("WHERE NodeId = @@NodeId and MediaId = @@MediaId");
            sb.AppendLine("");
            sb.AppendLine("END");
            sb.AppendLine("')");
            sb.AppendLine("END");

            SQLStatment = new Sql(sb.ToString());

            Database.Execute(SQLStatment);
        }
    }

    [TableName("ReflectionsUmbracoUtilitiesFileDownloadCounter")]
    [PrimaryKey("NodeId,MediaId")]
    public class ReflectionsUmbracoUtilitiesFileDownloadCounter
    {
        [Column("NodeId")]
        public int NodeId { get; set; }

        [Column("MediaId")]
        public int MediaId { get; set; }

        [Column("DownloadCounter")]
        public int DownloadCounter { get; set; }
    }
}
