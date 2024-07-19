using Mastery.Career.Domain;

namespace Mastery.Career.UnitTests;

public sealed class RankTests
{
    [Fact]
    public void Rank_is_correctly_created()
    {
        // Arrange
        string rankName = "A";
        RankId rankId = RankId.New(Guid.NewGuid());

        // Act
        Rank rank = Rank.New(rankId, rankName);

        // Assert
        rank.Id.Should().Be(rankId);
        rank.Name.Should().Be(rankName);
        rank.Color.Should().Be(Color.Default);
        rank.Description.Should().BeEmpty();
    }
}
