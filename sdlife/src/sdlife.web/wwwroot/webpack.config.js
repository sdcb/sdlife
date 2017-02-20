// wwwroot> webpack --watch

module.exports = {
    entry: "./app/main.js",
    output: {
        path: __dirname,
        filename: "webpack/bundle.js"
    },
    module: {
        loaders: [
            { test: /\.css$/, loader: "style!css" }
        ]
    }, 
    externals: {
        vue: "Vue", 
        "vue-router": "VueRouter"
    }
};