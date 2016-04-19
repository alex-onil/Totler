using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ApiConnection.Infrastructure;
using ApiConnection.Interfaces;
using AutoMapper;
using DataTypes.Models;
using DataTypes.ViewModels;

namespace WpfClient.Adapters
    {
    public class ApiModelGroupObservableAdapter : IApiCrud<ModelGroupViewModel, List<ModelGroupViewModel>>,
        IApiModelGroups<ModelGroupViewModel, List<ModelGroupViewModel>, List<ShippedProductViewModel>>
        {
        private readonly ApiModelGroups<List<ModelGroup>> _apiModelGroup;
        private readonly IMapper _mapper;

        public ApiModelGroupObservableAdapter(ApiModelGroups<List<ModelGroup>> apiModelGroup)
            {
            _apiModelGroup = apiModelGroup;
            _mapper = DataTypes.Mapper.MapperConfig.GetMapper();
            }

        public async Task<int> CountAsync()
            {
            return await _apiModelGroup.CountAsync();
            }

        public async Task<List<ModelGroupViewModel>> CreateAsync(IList<ModelGroupViewModel> items)
            {
            var buffer = _mapper.Map<ICollection<ModelGroupViewModel>, List<ModelGroup>>(items);

            var result = await _apiModelGroup.CreateAsync(buffer);

            return
                await Task.Run(() => _mapper.Map<List<ModelGroup>, List<ModelGroupViewModel>>(result));
            }

        public async Task<ModelGroupViewModel> CreateAsync(ModelGroupViewModel item)
            {
            var buffer = _mapper.Map<ModelGroupViewModel, ModelGroup>(item);
            var result = await _apiModelGroup.CreateAsync(buffer);
            return
                 await Task.Run(() => _mapper.Map<ModelGroup, ModelGroupViewModel>(result));
            }

        public async Task DeleteAsync(ModelGroupViewModel item)
            {
            var buffer = _mapper.Map<ModelGroupViewModel, ModelGroup>(item);
            await _apiModelGroup.DeleteAsync(buffer);
            }

        public async Task DeleteAsync(IList<ModelGroupViewModel> items)
            {
            var buffer = _mapper.Map<ICollection<ModelGroupViewModel>, List<ModelGroup>>(items);
            await _apiModelGroup.DeleteAsync(buffer);
            }

        public async Task<List<ModelGroupViewModel>> GetModelGroupsRootAsync()
            {
            var result = await _apiModelGroup.GetModelGroupsRootAsync();
            return
                await Task.Run(() => _mapper.Map<List<ModelGroup>, List<ModelGroupViewModel>>(result));
            }

        public async Task<List<ModelGroupViewModel>> ReadAsync(int startPosition = 0, int count = 0)
            {
            var result = await _apiModelGroup.ReadAsync(startPosition, count);
            return
                await Task.Run(() => _mapper.Map<List<ModelGroup>, List<ModelGroupViewModel>>(result));
            }

        public async Task<List<ModelGroupViewModel>> ReadModelChildsByModelGroupAsync(ModelGroupViewModel modelGroup,
                                                                                    int startPosition = 0, int count = 0)
            {
            var buffer = _mapper.Map<ModelGroupViewModel, ModelGroup>(modelGroup);
            var result = await _apiModelGroup.ReadModelChildsByModelGroupAsync(buffer, startPosition, count);
            return
                 await Task.Run(() => _mapper.Map<List<ModelGroup>, List<ModelGroupViewModel>>(result));
            }

        public async Task<List<ShippedProductViewModel>> ReadProductsByModelAsync(ModelGroupViewModel modelGroup, int startPosition = 0, int count = 0)
            {
            var buffer = _mapper.Map<ModelGroupViewModel, ModelGroup>(modelGroup);
            var result = await _apiModelGroup.ReadProductsByModelAsync(buffer, startPosition, count);
            return
                 await Task.Run(() => _mapper.Map<IList<ShippedProduct>, List<ShippedProductViewModel>>(result));
            }

        public async Task<ModelGroupViewModel> UpdateAsync(ModelGroupViewModel item)
            {
            var buffer = _mapper.Map<ModelGroupViewModel, ModelGroup>(item);
            var result = await _apiModelGroup.UpdateAsync(buffer);
            return
                 await Task.Run(() => _mapper.Map<ModelGroup, ModelGroupViewModel>(result));
            }

        public async Task<List<ModelGroupViewModel>> UpdateAsync(IList<ModelGroupViewModel> items)
            {
            var buffer = _mapper.Map<ICollection<ModelGroupViewModel>, List<ModelGroup>>(items);

            var result = await _apiModelGroup.UpdateAsync(buffer);

            return
                await Task.Run(() => _mapper.Map<List<ModelGroup>, List<ModelGroupViewModel>>(result));
            }
        }
    }
