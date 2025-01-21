// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  compatibilityDate: "2024-11-01",
  devtools: { enabled: true },
  modules: ["nuxt-open-fetch", "vuetify-nuxt-module"],
  runtimeConfig: {
    public: {
      openFetch: {
        backend: { baseURL: "http://localhost:5161" },
      },
    },
  },
  openFetch: {
    clients: {
      backend: { baseURL: "http://localhost:5161" },
    },
  },
} as any);

