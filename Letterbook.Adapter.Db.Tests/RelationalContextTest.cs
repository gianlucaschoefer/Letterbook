using System.Diagnostics;
using Letterbook.Core.Tests;
using Letterbook.Core.Tests.Fakes;
using Letterbook.Core.Values;
using Medo;
using Microsoft.Extensions.Options;

namespace Letterbook.Adapter.Db.Tests;

public class RelationalContextTest : IDisposable
{
    public void Dispose() => _context.Dispose();
    private readonly RelationalContext _context;

    public RelationalContextTest()
    {
        _context = new RelationalContext(Options.Create(new DbOptions
        {
            Host = "localhost",
            Port = "5432",
            Username = "postgres",
            Database = "postgres",
            UseSsl = false,
            Password = "postgres",
        }));
    }
    
    [Fact]
    public void CanTrackAccounts()
    {
        var account = new FakeAccount().Generate();
        _context.Add(account);
    }

    [Fact]
    public void CanTrackProfiles()
    {
        var profile = new FakeProfile().Generate();
        _context.Add(profile);
    }

    [Fact]
    public void CanTrackFollows()
    {
        var profile = new FakeProfile("letterbook.example").Generate();
        var follower = new FakeProfile().Generate();
        profile.AddFollower(follower, FollowState.Accepted);
        
        _context.Add(profile);
    }
}