using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Jobby.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ApplicantRelatedEntitiesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_question_options_question_id",
                table: "question_options");

            migrationBuilder.DropIndex(
                name: "ix_applicants_vacancy_id",
                table: "applicants");

            migrationBuilder.CreateTable(
                name: "applicant_answers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicant_id = table.Column<int>(type: "integer", nullable: false),
                    question_id = table.Column<int>(type: "integer", nullable: false),
                    question_option_id = table.Column<int>(type: "integer", nullable: true),
                    is_correct = table.Column<bool>(type: "boolean", nullable: false),
                    answered_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: false),
                    updated_by_id = table.Column<int>(type: "integer", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_applicant_answers", x => x.id);
                    table.ForeignKey(
                        name: "fk_applicant_answers_applicants_applicant_id",
                        column: x => x.applicant_id,
                        principalTable: "applicants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_applicant_answers_question_options_question_option_id",
                        column: x => x.question_option_id,
                        principalTable: "question_options",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_applicant_answers_questions_question_id",
                        column: x => x.question_id,
                        principalTable: "questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "applicant_question_progresses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicant_id = table.Column<int>(type: "integer", nullable: false),
                    question_id = table.Column<int>(type: "integer", nullable: false),
                    question_started_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    question_expires_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_answered = table.Column<bool>(type: "boolean", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: false),
                    updated_by_id = table.Column<int>(type: "integer", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_applicant_question_progresses", x => x.id);
                    table.ForeignKey(
                        name: "fk_applicant_question_progresses_applicants_applicant_id",
                        column: x => x.applicant_id,
                        principalTable: "applicants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_applicant_question_progresses_questions_question_id",
                        column: x => x.question_id,
                        principalTable: "questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "test_results",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicant_id = table.Column<int>(type: "integer", nullable: false),
                    total_questions = table.Column<int>(type: "integer", nullable: false),
                    correct_answers = table.Column<int>(type: "integer", nullable: false),
                    wrong_answers = table.Column<int>(type: "integer", nullable: false),
                    score_percent = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    completed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: false),
                    updated_by_id = table.Column<int>(type: "integer", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_test_results", x => x.id);
                    table.ForeignKey(
                        name: "fk_test_results_applicants_applicant_id",
                        column: x => x.applicant_id,
                        principalTable: "applicants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_question_options_question_id_is_correct",
                table: "question_options",
                columns: new[] { "question_id", "is_correct" });

            migrationBuilder.CreateIndex(
                name: "ix_applicants_vacancy_id_email",
                table: "applicants",
                columns: new[] { "vacancy_id", "email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_applicant_answers_applicant_id_question_id",
                table: "applicant_answers",
                columns: new[] { "applicant_id", "question_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_applicant_answers_question_id",
                table: "applicant_answers",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "ix_applicant_answers_question_option_id",
                table: "applicant_answers",
                column: "question_option_id");

            migrationBuilder.CreateIndex(
                name: "ix_applicant_question_progresses_applicant_id_question_id",
                table: "applicant_question_progresses",
                columns: new[] { "applicant_id", "question_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_applicant_question_progresses_question_id",
                table: "applicant_question_progresses",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "ix_test_results_applicant_id",
                table: "test_results",
                column: "applicant_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applicant_answers");

            migrationBuilder.DropTable(
                name: "applicant_question_progresses");

            migrationBuilder.DropTable(
                name: "test_results");

            migrationBuilder.DropIndex(
                name: "ix_question_options_question_id_is_correct",
                table: "question_options");

            migrationBuilder.DropIndex(
                name: "ix_applicants_vacancy_id_email",
                table: "applicants");

            migrationBuilder.CreateIndex(
                name: "ix_question_options_question_id",
                table: "question_options",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "ix_applicants_vacancy_id",
                table: "applicants",
                column: "vacancy_id");
        }
    }
}
