// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  compatibilityDate: "2025-01-21",
  devtools: { enabled: true },
  modules: ["@vee-validate/nuxt", "nuxt-open-fetch", "vuetify-nuxt-module"],
  runtimeConfig: {
    public: {
      backend: {
        baseURL: "http://localhost:5161",
      },
    },
  },
  openFetch: {
    clients: {
      backend: { baseURL: "/api/backend" },
    },
  },
});