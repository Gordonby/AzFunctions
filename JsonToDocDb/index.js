module.exports = function (context, data) {
        
  context.bindings.outputDocument = data.body;
        
  context.res = {
  // status: 200, /* Defaults to 200 */
  };
                
  context.done();
                
};