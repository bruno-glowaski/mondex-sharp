export default defineEventHandler((event) => {
  const path = event.context.params?.slug;
  const backendUrl = useRuntimeConfig().public.backend.baseURL;
  console.log("Redirecting", path, "to", backendUrl);
  return proxyRequest(event, `${backendUrl}/${path}`);
});
