const PREFIX = "/api/backend/";
export default defineEventHandler((event) => {
  const path = event.path.slice(PREFIX.length);
  const query = getQuery(event);
  const backendUrl = useRuntimeConfig().public.backend.baseURL;
  const finalUrl = `${backendUrl}/${path}`;
  console.log("Redirecting", path, "to", finalUrl);
  return proxyRequest(event, finalUrl);
});
