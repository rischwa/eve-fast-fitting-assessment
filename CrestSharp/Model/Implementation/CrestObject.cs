using System;
using System.Threading.Tasks;
using CrestSharp.Implementation;
using CrestSharp.Infrastructure;
using Newtonsoft.Json;
using Serilog;

namespace CrestSharp.Model.Implementation
{
    public abstract class CrestObject : ICrestObject, IIsInitializable
    {
        private bool _isInitialized;
        private string _href;

        // ReSharper disable once ConvertToAutoProperty
        public virtual string Href
        {
            get { return _href; }
            set { _href = value; }
        }

        public bool IsInitialized => _isInitialized;

        public void EnsureInitialization()
        {
            lock (this)
            {
                if (_isInitialized)
                {
                    return;
                }
                Refresh();
            }
        }

        public void Refresh()
        {
            if (Href == null)
            {
                throw new Exception("Trying to load CrestObject without Href value");
            }

            try
            {
                var document =  Crest.Settings.DocumentLoader.LoadDocumentAsync(Href).Result;

                JsonConvert.PopulateObject(document, this, CrestCacheHelper.SETTINGS);
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, $"Could not load {Href}.");
            }

            _isInitialized = true;
            Crest.Settings.Cache.Put(this);
        }

        public async Task RefreshAsync()
        {
            if (Href == null)
            {
                throw new Exception("Trying to load CrestObject without Href value");
            }

            try
            {
                var document = await Crest.Settings.DocumentLoader.LoadDocumentAsync(Href);

                JsonConvert.PopulateObject(document, this, CrestCacheHelper.SETTINGS);
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, $"Could not load {Href}.");
                throw;
            }

            _isInitialized = true;
            await Crest.Settings.Cache.Put(this);
        }

        protected T EnsureLoadedValue<T>(ref T field)
        {
            lock (this)
            {
                if (field == null && !_isInitialized)
                {
                    Refresh();
                }

                return field;
            }
        }

        protected T EnsureLoadedValue<T>(ref T? field) where T : struct
        {
            lock (this)
            {
                if (field == null && !_isInitialized)
                {
                    Refresh();
                }

                // ReSharper disable once PossibleInvalidOperationException
                return field.GetValueOrDefault();
            }
        }
        
    }
}
