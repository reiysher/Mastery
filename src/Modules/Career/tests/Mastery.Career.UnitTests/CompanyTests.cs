using Mastery.Career.Domain;

namespace Mastery.Career.UnitTests;

public sealed class CompanyTests
{
    [Fact]
    public void The_company_is_correctly_created()
    {
        // Arrange
        CompanyId companyId = new CompanyId(Guid.NewGuid());

        // Act
        Company sut = Company.New(companyId);

        // Assert
        sut.Id.Should().Be(companyId);
        sut.RankId.Should().BeNull();
        sut.PlatformRanks.Should().BeEmpty();
    }

    [Fact]
    public void Company_id_does_not_created_with_default_value()
    {
        // Arrange
        Guid value = Guid.Empty;

        // Act
        var action = () => new CompanyId(value);

        // Assert
        action.Should().Throw<ArgumentException>().Which
            .Message.Should().Be(CompanyExceptions.InvalidIdInput);
    }

    [Fact]
    public void Company_rank_was_changed()
    {
        // Arrange
        RankId rankId = RankId.Random();
        Company sut = Company.New(new CompanyId(Guid.NewGuid()));

        // Act
        sut.SetRank(rankId);

        // Assert
        sut.RankId.Should().Be(rankId);
    }
}
