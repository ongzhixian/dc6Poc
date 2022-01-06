using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace Dn6Poc.DocuMgmtPortal.Tests.Helpers;

internal class SessionSubstitue : ISession
{
    private readonly Dictionary<string, byte[]?> _dataStore = new Dictionary<string, byte[]?>();

    public SessionSubstitue()
    {
    }

    public bool IsAvailable => true;

    public string Id => string.Empty;

    public IEnumerable<string> Keys => _dataStore.Keys;

    public void Clear()
    {
        _dataStore.Clear();
    }

    public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

    public Task LoadAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

    public void Remove(string key)
    {
        _dataStore.Remove(key);
    }

    public void Set(string key, byte[] value)
    {
        _dataStore[key] = value;
    }

    public bool TryGetValue(string key, [NotNullWhen(true)] out byte[]? value)
    {
        return _dataStore.TryGetValue(key, out value);
    }
}
