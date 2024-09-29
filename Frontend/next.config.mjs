import { setupDevPlatform } from "@cloudflare/next-on-pages/next-dev"

if (process.env.NEXT_PUBLIC_ENV === "development") {
  setupDevPlatform({
    persist: true,
  })
}
/** @type {import('next').NextConfig} */
const nextConfig = {
  env: {
    PUBLIC_NEXT_BASE_URL: process.env.PUBLIC_NEXT_BASE_URL,
  },
}

export default nextConfig
