using Microsoft.JSInterop;
using PrinterCell.Client.Models;

namespace PrinterCell.Client.Services
{

    public class IndexedDbService
    {
        private readonly IJSRuntime _js;

        public IndexedDbService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task InitAsync()
        {
            await _js.InvokeVoidAsync("indexedDbManager.init");
        }

        public async Task<List<Articolo>> GetAllArticoliAsync()
        {
            return await _js.InvokeAsync<List<Articolo>>("indexedDbManager.getAll");
        }

        public async Task<int> AddArticoloAsync(Articolo articolo)
        {
            return await _js.InvokeAsync<int>("indexedDbManager.add", articolo);
        }

        public async Task UpdateArticoloAsync(Articolo articolo)
        {
            await _js.InvokeVoidAsync("indexedDbManager.update", articolo);
        }

        public async Task DeleteArticoloAsync(int id)
        {
            await _js.InvokeVoidAsync("indexedDbManager.delete", id);
        }
    }
}
