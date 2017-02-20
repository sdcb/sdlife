// wwwroot> webpack --config webpack.config.production.js -p

var devConfig = require("./webpack.config");
devConfig.output.filename = "webpack/bundle.min.js";
module.exports = devConfig;