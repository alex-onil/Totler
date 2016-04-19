using System.Collections.ObjectModel;
using ApiConnection;
using DataTypes.ViewModels;
using WpfClient.Adapters;
using WpfClient.Domain;

namespace WpfClient.Commands.Abstract
    {
    /// <summary>
    /// Контекст для обработчиков комманд
    /// </summary>
    public class CommandContext
        {
        public ApiClient Client { get; }
        public ApiShippedProductsObservableAdapter ApiShippedProducts { get; }
        public ApiModelGroupObservableAdapter ApiModelGroups {get;}
        public ObservableCollectionRange<ModelGroupViewModel> ModelGroupCollection { get; }
        public ObservableCollectionRange<ShippedProductViewModel> ShippedproductsCollection { get; }
        internal CommandContext(ApiClient client, 
                                ApiShippedProductsObservableAdapter apiShippedProducts,
                                ApiModelGroupObservableAdapter apiModelGroups,
                                ObservableCollectionRange<ModelGroupViewModel> modelGroupCollection,
                                ObservableCollectionRange<ShippedProductViewModel> shippedproductsCollection)
            {
             ModelGroupCollection = modelGroupCollection;
             ShippedproductsCollection = shippedproductsCollection;
            ApiShippedProducts = apiShippedProducts;
            ApiModelGroups = apiModelGroups;
            Client = client;
            }
        }
    }
