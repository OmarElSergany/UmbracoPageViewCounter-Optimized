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
    //class PageViewCounterInstall : ApplicationEventHandler
    //{
    //    protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
    //    {
    //        //HandleReflectionsUmbracoUtilitiesPageViewCounterMigration();
    //        //Get the Umbraco Database context
    //        var ctx = applicationContext.DatabaseContext;
    //        var db = new DatabaseSchemaHelper(ctx.Database, applicationContext.ProfilingLogger.Logger, ctx.SqlSyntax);

    //        //Check if the DB table does NOT exist
    //        if (!db.TableExist("ReflectionsUmbracoUtilitiesPageViewCounter"))
    //        {
    //            //Create DB table - and set overwrite to false
    //            db.CreateTable<ReflectionsUmbracoUtilitiesPageViewCounter>(false);

    //            StringBuilder sb = new StringBuilder(string.Empty);

    //            sb.AppendLine("IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReflectionsUmbracoUtilitiesSetPageViewCount]') AND type in (N'P', N'PC'))");
    //            sb.AppendLine("BEGIN");
    //            sb.AppendLine("execute ('");
    //            sb.AppendLine("CREATE PROCEDURE [dbo].[ReflectionsUmbracoUtilitiesSetPageViewCount]");
    //            sb.AppendLine("(");
    //            sb.AppendLine("@@NodeId INT");
    //            sb.AppendLine(")");
    //            sb.AppendLine("AS");
    //            sb.AppendLine("BEGIN");
    //            sb.AppendLine("SET NOCOUNT ON;");
    //            sb.AppendLine("IF EXISTS(SELECT 1 FROM ReflectionsUmbracoUtilitiesPageViewCounter WHERE NodeId = @@NodeId)");
    //            sb.AppendLine("BEGIN");
    //            sb.AppendLine("UPDATE ReflectionsUmbracoUtilitiesPageViewCounter SET ViewsCounter = ViewsCounter + 1");
    //            sb.AppendLine("WHERE NodeId = @@NodeId");
    //            sb.AppendLine("END");
    //            sb.AppendLine("ELSE");
    //            sb.AppendLine("BEGIN");
    //            sb.AppendLine("INSERT INTO ReflectionsUmbracoUtilitiesPageViewCounter(NodeId, ViewsCounter) VALUES (@@NodeId, 1)");
    //            sb.AppendLine("END");
    //            sb.AppendLine("END");
    //            sb.AppendLine("')");
    //            sb.AppendLine("END");

    //            var SQLStatment = new Sql(sb.ToString());
    //            ctx.Database.Execute(SQLStatment);


    //            sb.Clear();

    //            sb.AppendLine("IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReflectionsUmbracoUtilitiesGetPageViewCount]') AND type in (N'P', N'PC'))");
    //            sb.AppendLine("BEGIN");
    //            sb.AppendLine("execute ('");
    //            sb.AppendLine("CREATE PROCEDURE [dbo].[ReflectionsUmbracoUtilitiesGetPageViewCount]");
    //            sb.AppendLine("(");
    //            sb.AppendLine("@@NodeId INT");
    //            sb.AppendLine(")");
    //            sb.AppendLine("AS");
    //            sb.AppendLine("BEGIN");
    //            sb.AppendLine("");
    //            sb.AppendLine("SET NOCOUNT ON;");
    //            sb.AppendLine("");
    //            sb.AppendLine("SELECT ViewsCounter");
    //            sb.AppendLine("FROM ReflectionsUmbracoUtilitiesPageViewCounter");
    //            sb.AppendLine("WHERE NodeId = @@NodeId");
    //            sb.AppendLine("");
    //            sb.AppendLine("END");
    //            sb.AppendLine("')");
    //            sb.AppendLine("END");

    //            SQLStatment = new Sql(sb.ToString());

    //            ctx.Database.Execute(SQLStatment);
    //        }
    //    }

    //    //private static void HandleReflectionsUmbracoUtilitiesPageViewCounterMigration()
    //    //{
    //    //    const string PluginName = "ReflectionsUmbracoUtilitiesPageViewCounterMigration";
    //    //    var currentVersion = new SemVersion(0, 0, 0);

    //    //    // get all migrations for "Statistics" already executed
    //    //    var migrations = ApplicationContext.Current.Services.MigrationEntryService.GetAll(PluginName);

    //    //    // get the latest migration for "Statistics" executed
    //    //    var latestMigration = migrations.OrderByDescending(x => x.Version).FirstOrDefault();

    //    //    if (latestMigration != null)
    //    //        currentVersion = latestMigration.Version;

    //    //    var targetVersion = new SemVersion(1, 0, 0);
    //    //    if (targetVersion == currentVersion)
    //    //        return;

    //    //    var migrationsRunner = new MigrationRunner(
    //    //      ApplicationContext.Current.Services.MigrationEntryService,
    //    //      ApplicationContext.Current.ProfilingLogger.Logger,
    //    //      currentVersion,
    //    //      targetVersion,
    //    //      PluginName);

    //    //    try
    //    //    {
    //    //        migrationsRunner.Execute(Umbraco.Web.UmbracoContext.Current.Application.DatabaseContext.Database);
    //    //    }
    //    //    catch (Exception e)
    //    //    {
    //    //        LogHelper.Error<MigrationEvents>("Error running Reflections Umbraco Utilities Page View Counter migration", e);
    //    //    }
    //    //}

    //    //[Migration("1.0.0", 1, "ReflectionsUmbracoUtilitiesPageViewCounter")]
    //    //public class CreateReflectionsUmbracoUtilitiesPageViewCounterTable : MigrationBase
    //    //{
    //    //    private readonly UmbracoDatabase database = ApplicationContext.Current.DatabaseContext.Database;
    //    //    private readonly DatabaseSchemaHelper schemaHelper;

    //    //    public CreateReflectionsUmbracoUtilitiesPageViewCounterTable(ISqlSyntaxProvider sqlSyntax, ILogger logger)
    //    //      : base(sqlSyntax, logger)
    //    //    {
    //    //        schemaHelper = new DatabaseSchemaHelper(database, logger, sqlSyntax);
    //    //    }

    //    //    public override void Up()
    //    //    {
    //    //        schemaHelper.CreateTable<ReflectionsUmbracoUtilitiesPageViewCounter>(false);

    //    //        // Remember you can execute ANY code here and in Down().. 
    //    //        // Anything you can think of, go nuts (not really!)
    //    //    }

    //    //    public override void Down()
    //    //    {
    //    //        schemaHelper.DropTable<ReflectionsUmbracoUtilitiesPageViewCounter>();
    //    //    }
    //    //}

    //}

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
            sb.AppendLine("SELECT ViewsCounter");
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
