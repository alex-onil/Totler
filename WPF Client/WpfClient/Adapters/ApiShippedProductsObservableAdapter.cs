using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ApiConnection.Interfaces;
using AutoMapper;
using DataTypes.Models;
using DataTypes.ViewModels;

namespace WpfClient.Adapters
    {
    public class ApiShippedProductsObservableAdapter : IApiCrud<ShippedProductViewModel, List<ShippedProductViewModel>>
        {
        private readonly IApiCrud<ShippedProduct,List<ShippedProduct>> _apiShippedProducts;
        private readonly IMapper _mapper;

        public ApiShippedProductsObservableAdapter(IApiCrud<ShippedProduct, List<ShippedProduct>> apiShippedProducts)
        {
            _apiShippedProducts = apiShippedProducts;
            _mapper = DataTypes.Mapper.MapperConfig.GetMapper();
            }


        public async Task<ShippedProductViewModel> CreateAsync(ShippedProductViewModel item)
        {
            var buffer = _mapper.Map<ShippedProductViewModel, ShippedProduct>(item);

            var result = await _apiShippedProducts.CreateAsync(buffer);

            return
                await Task.Run(() => _mapper.Map<ShippedProduct, ShippedProductViewModel>(result));
            }

        public async Task<List<ShippedProductViewModel>> CreateAsync(IList<ShippedProductViewModel> items)
        {
            var buffer = _mapper.Map<ICollection<ShippedProductViewModel>, List<ShippedProduct>>(items);

            var result = await _apiShippedProducts.CreateAsync(buffer);

            return
                await Task.Run(() => _mapper.Map<List<ShippedProduct>, List<ShippedProductViewModel>>(result));
            }

        public async Task<List<ShippedProductViewModel>> ReadAsync(int startPosition = 0, int count = 0)
        {
            var result = await _apiShippedProducts.ReadAsync(startPosition, count);

            return
                await Task.Run(() => _mapper.Map<List<ShippedProduct>,List<ShippedProductViewModel>>(result));
            }

        public async Task<int> CountAsync()
        {
            return await _apiShippedProducts.CountAsync();
        }

        public async Task<List<ShippedProductViewModel>> UpdateAsync(IList<ShippedProductViewModel> items)
        {
            var buffer = _mapper.Map<ICollection<ShippedProductViewModel>, List<ShippedProduct>>(items);

            var result = await _apiShippedProducts.UpdateAsync(buffer);

            return
                await Task.Run(() => _mapper.Map<List<ShippedProduct>, List<ShippedProductViewModel>>(result));
            }

        public async Task<ShippedProductViewModel> UpdateAsync(ShippedProductViewModel item)
        {
            var buffer = _mapper.Map<ShippedProductViewModel, ShippedProduct>(item);

            var result = await _apiShippedProducts.UpdateAsync(buffer);

            return
                await Task.Run(() => _mapper.Map<ShippedProduct, ShippedProductViewModel>(result));
            }

        public async Task DeleteAsync(IList<ShippedProductViewModel> items)
        {
            var buffer = _mapper.Map<ICollection<ShippedProductViewModel>, List<ShippedProduct>>(items);

            await _apiShippedProducts.DeleteAsync(buffer);
            }

        public async Task DeleteAsync(ShippedProductViewModel item)
        {
            var buffer = _mapper.Map<ShippedProductViewModel, ShippedProduct>(item);

            await _apiShippedProducts.DeleteAsync(buffer);
            }
        }
    }
