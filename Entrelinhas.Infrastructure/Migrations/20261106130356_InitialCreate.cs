using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entrelinhas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "book",
                columns: table => new
                {
                    book_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    publication_date = table.Column<DateOnly>(type: "date", nullable: true),
                    author = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    api_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_book", x => x.book_id);
                });

            migrationBuilder.CreateTable(
                name: "club",
                columns: table => new
                {
                    club_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_club", x => x.club_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "cycle",
                columns: table => new
                {
                    cycle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    due = table.Column<DateOnly>(type: "date", nullable: false),
                    club_id = table.Column<Guid>(type: "uuid", nullable: false),
                    book_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cycle", x => x.cycle_id);
                    table.CheckConstraint("ck_cycle_due_after_start", "due >= start_date");
                    table.ForeignKey(
                        name: "fk_cycle_book_id_book",
                        column: x => x.book_id,
                        principalTable: "book",
                        principalColumn: "book_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cycle_club_id_club",
                        column: x => x.club_id,
                        principalTable: "club",
                        principalColumn: "club_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "poll",
                columns: table => new
                {
                    poll_id = table.Column<Guid>(type: "uuid", nullable: false),
                    closes_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    type = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    club_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_poll", x => x.poll_id);
                    table.ForeignKey(
                        name: "fk_poll_club_id_club",
                        column: x => x.club_id,
                        principalTable: "club",
                        principalColumn: "club_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "bookcase",
                columns: table => new
                {
                    bookcase_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: true),
                    review = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    book_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bookcase", x => x.bookcase_id);
                    table.ForeignKey(
                        name: "fk_bookcase_book_id_book",
                        column: x => x.book_id,
                        principalTable: "book",
                        principalColumn: "book_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_bookcase_user_id_users",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "club_member",
                columns: table => new
                {
                    club_member_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    club_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_club_member", x => x.club_member_id);
                    table.ForeignKey(
                        name: "fk_club_member_club_id_club",
                        column: x => x.club_id,
                        principalTable: "club",
                        principalColumn: "club_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_club_member_user_id_users",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "follower",
                columns: table => new
                {
                    follow_id = table.Column<Guid>(type: "uuid", nullable: false),
                    follower_id = table.Column<Guid>(type: "uuid", nullable: false),
                    followed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_follower", x => x.follow_id);
                    table.CheckConstraint("ck_follower_not_self", "follower_id <> followed_id");
                    table.ForeignKey(
                        name: "fk_follower_followed_id_users",
                        column: x => x.followed_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_follower_follower_id_users",
                        column: x => x.follower_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "comment",
                columns: table => new
                {
                    comment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cycle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    chapter = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    text = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comment", x => x.comment_id);
                    table.ForeignKey(
                        name: "fk_comment_cycle_id_cycle",
                        column: x => x.cycle_id,
                        principalTable: "cycle",
                        principalColumn: "cycle_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_comment_user_id_users",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "poll_option",
                columns: table => new
                {
                    poll_option_id = table.Column<Guid>(type: "uuid", nullable: false),
                    poll_id = table.Column<Guid>(type: "uuid", nullable: false),
                    book_id = table.Column<Guid>(type: "uuid", nullable: true),
                    deadline = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_poll_option", x => x.poll_option_id);
                    table.UniqueConstraint("ak_poll_option_poll_option_id_poll_id", x => new { x.poll_option_id, x.poll_id });
                    table.CheckConstraint("ck_poll_option_book_xor_deadline", "(book_id IS NULL) <> (deadline IS NULL)");
                    table.ForeignKey(
                        name: "fk_poll_option_book_id_book",
                        column: x => x.book_id,
                        principalTable: "book",
                        principalColumn: "book_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_poll_option_poll_id_poll",
                        column: x => x.poll_id,
                        principalTable: "poll",
                        principalColumn: "poll_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cycle_participant",
                columns: table => new
                {
                    cycle_participant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    reason = table.Column<string>(type: "text", nullable: true),
                    club_member_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cycle_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cycle_participant", x => x.cycle_participant_id);
                    table.ForeignKey(
                        name: "fk_cycle_participant_club_member_id_club_member",
                        column: x => x.club_member_id,
                        principalTable: "club_member",
                        principalColumn: "club_member_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cycle_participant_cycle_id_cycle",
                        column: x => x.cycle_id,
                        principalTable: "cycle",
                        principalColumn: "cycle_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "vote",
                columns: table => new
                {
                    vote_id = table.Column<Guid>(type: "uuid", nullable: false),
                    poll_id = table.Column<Guid>(type: "uuid", nullable: false),
                    poll_option_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vote", x => x.vote_id);
                    table.ForeignKey(
                        name: "fk_vote_poll_id_poll",
                        column: x => x.poll_id,
                        principalTable: "poll",
                        principalColumn: "poll_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_vote_poll_option_id_poll_option",
                        columns: x => new { x.poll_option_id, x.poll_id },
                        principalTable: "poll_option",
                        principalColumns: new[] { "poll_option_id", "poll_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_vote_user_id_users",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_book_api_id",
                table: "book",
                column: "api_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_bookcase_book_id",
                table: "bookcase",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "ix_bookcase_user_id_book_id",
                table: "bookcase",
                columns: new[] { "user_id", "book_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_club_member_club_id",
                table: "club_member",
                column: "club_id");

            migrationBuilder.CreateIndex(
                name: "ix_club_member_user_id_club_id",
                table: "club_member",
                columns: new[] { "user_id", "club_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_comment_cycle_id",
                table: "comment",
                column: "cycle_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_user_id",
                table: "comment",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_cycle_book_id",
                table: "cycle",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "ix_cycle_club_id",
                table: "cycle",
                column: "club_id");

            migrationBuilder.CreateIndex(
                name: "ix_cycle_participant_club_member_id_cycle_id",
                table: "cycle_participant",
                columns: new[] { "club_member_id", "cycle_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_cycle_participant_cycle_id",
                table: "cycle_participant",
                column: "cycle_id");

            migrationBuilder.CreateIndex(
                name: "ix_follower_followed_id",
                table: "follower",
                column: "followed_id");

            migrationBuilder.CreateIndex(
                name: "ix_follower_follower_id_followed_id",
                table: "follower",
                columns: new[] { "follower_id", "followed_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_poll_club_id",
                table: "poll",
                column: "club_id");

            migrationBuilder.CreateIndex(
                name: "ix_poll_option_book_id",
                table: "poll_option",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "ix_poll_option_poll_id_book_id",
                table: "poll_option",
                columns: new[] { "poll_id", "book_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_poll_option_poll_id_deadline",
                table: "poll_option",
                columns: new[] { "poll_id", "deadline" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_vote_poll_id_user_id",
                table: "vote",
                columns: new[] { "poll_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_vote_poll_option_id_poll_id",
                table: "vote",
                columns: new[] { "poll_option_id", "poll_id" });

            migrationBuilder.CreateIndex(
                name: "ix_vote_user_id",
                table: "vote",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bookcase");

            migrationBuilder.DropTable(
                name: "comment");

            migrationBuilder.DropTable(
                name: "cycle_participant");

            migrationBuilder.DropTable(
                name: "follower");

            migrationBuilder.DropTable(
                name: "vote");

            migrationBuilder.DropTable(
                name: "club_member");

            migrationBuilder.DropTable(
                name: "cycle");

            migrationBuilder.DropTable(
                name: "poll_option");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "book");

            migrationBuilder.DropTable(
                name: "poll");

            migrationBuilder.DropTable(
                name: "club");
        }
    }
}
