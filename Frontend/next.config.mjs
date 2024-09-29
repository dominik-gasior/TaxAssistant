import { setupDevPlatform } from "@cloudflare/next-on-pages/next-dev"

if (process.env.NEXT_PUBLIC_ENV === "development") {
  setupDevPlatform({
    persist: true,
  })
}
/** @type {import('next').NextConfig} */
const nextConfig = {}

export default nextConfig
