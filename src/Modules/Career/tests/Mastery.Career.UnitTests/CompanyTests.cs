using Mastery.Career.Domain;

namespace Mastery.Career.UnitTests;

public sealed class CompanyTests
{
    [Fact]
    public void The_company_is_correctly_created()
    {
        // Arrange
        Guid companyId = Guid.NewGuid();

        // Act
        Company sut = Company.New(companyId);

        // Assert
        sut.Id.Should().Be(companyId);
        sut.RankId.Should().BeNull();
    }

    [Fact]
    public void Company_rank_was_changed()
    {
        // Arrange
        Guid rankId = Guid.NewGuid();
        Company sut = Company.New(Guid.NewGuid());

        // Act
        sut.SetRank(rankId);

        // Assert
        sut.RankId.Should().Be(rankId);
    }
}
