using System;
using NServiceKit.Text;

namespace SharpBucket{
    public class ConfigScope : IDisposable{
        private readonly JsConfigScope jsConfigScope;

        public ConfigScope(){
            jsConfigScope = JsConfig.With(
                emitLowercaseUnderscoreNames: true,
                emitCamelCaseNames: false);
        }

        public void Dispose(){
            jsConfigScope.Dispose();
        }
    }
}