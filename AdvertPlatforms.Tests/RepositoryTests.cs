using System.Text;
using Xunit;
using FluentAssertions;
using AdvertPlatforms.Api.Services;

namespace AdvertPlatforms.Tests
{
    public class RepositoryTests
    {
        private static MemoryStream MsFrom(string text) =>
        new MemoryStream(Encoding.UTF8.GetBytes(text));

        [Fact]
        public async Task Load_ValidFile_And_Search_Succeeds()
        {
            var repo = new PlatformRepository();
            string data = """
                    Яндекс.Директ:/ru
                    Ревдинский рабочий:/ru/svrd/revda, /ru/svrd/pervik
                    Газета уральских москвичей:/ru/msk, /ru/permobl, /ru/chelobl
                    Крутая реклама:/ru/svrd
                    """;
            await repo.LoadFromStreamAsync(MsFrom(data));

            var r1 = repo.SearchByLocation("/ru/svrd/revda");
            r1.Should().BeEquivalentTo(new[] { "Крутая реклама", "Ревдинский рабочий", "Яндекс.Директ" });

            var r2 = repo.SearchByLocation("/ru/svrd");
            r2.Should().BeEquivalentTo(new[] { "Крутая реклама", "Яндекс.Директ" });

            var r3 = repo.SearchByLocation("/ru/msk");
            r3.Should().BeEquivalentTo(new[] { "Газета уральских москвичей", "Яндекс.Директ" });
        }

        [Fact]
        public async Task Load_InvalidLine_NoColon_Throws()
        {
            var repo = new PlatformRepository();
            string data = "ПлохаяСтрокаБезДвоеточия";
            Func<Task> act = async () => await repo.LoadFromStreamAsync(MsFrom(data));
            await act.Should().ThrowAsync<FormatException>()
                .WithMessage("*двоеточ*");
        }

        [Fact]
        public async Task Load_ReplacesOldData()
        {
            var repo = new PlatformRepository();
            string data1 = """
                        A:/ru
                        B:/ru/svrd
                        """;
                            string data2 = """
                        X:/ru/chel
                        """;
            await repo.LoadFromStreamAsync(MsFrom(data1));
            var before = repo.SearchByLocation("/ru/svrd");
            before.Should().BeEquivalentTo(new[] { "A", "B" });

            await repo.LoadFromStreamAsync(MsFrom(data2));
            var after = repo.SearchByLocation("/ru/svrd");
            after.Should().BeEmpty();

            var chel = repo.SearchByLocation("/ru/chel");
            chel.Should().BeEquivalentTo(new[] { "X" });
        }

        [Fact]
        public async Task Search_UnknownLocation_ReturnsEmpty()
        {
            var repo = new PlatformRepository();
            string data = "A:/ru\n";
            await repo.LoadFromStreamAsync(MsFrom(data));
            var res = repo.SearchByLocation("/us/ny");
            res.Should().BeEmpty();
        }
    }
}
