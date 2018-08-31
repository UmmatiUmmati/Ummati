module.exports = {
  configureWebpack: {
    devtool: "source-map"
  },

  css: {
    loaderOptions: {
      sass: {
        data: `@import "@/styles/site.scss";`
      }
    },
    sourceMap: true
  },

  // See https://github.com/vuejs/vue-cli/tree/dev/packages/%40vue/cli-plugin-pwa
  pwa: {
    // manifest.json
    name: "Ummati",
    themeColor: "#0ebfe9",
    msTileColor: "#0ebfe9",

    // Workbox
    // See https://developers.google.com/web/tools/workbox/modules/workbox-webpack-plugin
    workboxOptions: {
      // See https://github.com/GoogleChrome/workbox/issues/1614
      // workboxPluginMode: "InjectManifest",
      // swSrc: "./src/service-worker.js",

      // Import Workbox library
      importWorkboxFrom: "local",
      importsDirectory: "js/sw",

      // Static caching
      exclude: [/\.map$/, /\.txt$/, /\.xml$/, /\.ico$/, /manifest\.json$/]

      // Runtime caching
      // runtimeCaching: [
      //   {
      //     urlPattern: /^http(s?):\/\/(foo|bar)(.*)$/,
      //     // Apply a network-first strategy.
      //     handler: "networkFirst",
      //     options: {
      //       // Fall back to the cache after 5 seconds.
      //       networkTimeoutSeconds: 5,
      //       // Use a custom cache name for this route.
      //       cacheName: "api-cache",
      //       // Configure custom cache expiration.
      //       expiration: {
      //         maxEntries: 50,
      //         maxAgeSeconds: 604800 // 7 days
      //       },
      //       // Configure which responses are considered cacheable.
      //       cacheableResponse: {
      //         statuses: [0, 200]
      //         // headers: {'x-test': 'true'},
      //       }
      //       // Configure the broadcast cache update plugin.
      //       // broadcastUpdate: {
      //       //   channelName: 'update-channel',
      //       // }
      //       // Add in any additional plugin logic you need.
      //       // plugins: [
      //       //   {cacheDidUpdate: () => /* custom plugin code */}
      //       // ],
      //     }
      //   }
      // ]
    }
  }
};
