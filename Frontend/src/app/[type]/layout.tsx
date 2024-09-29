import { Metadata } from "next"

export function generateMetadata({
  params,
}: {
  params: { type: string }
}): Metadata {
  const formattedType =
    params.type.charAt(0).toUpperCase() + params.type.slice(1)
  return {
    title: `${formattedType} Declaration Form`,
    description: `Fill out the ${formattedType} declaration form with our interactive assistant.`,
    openGraph: {
      title: `${formattedType} Declaration Form`,
      description: `Fill out the ${formattedType} declaration form with our interactive assistant.`,
      type: "website",
    },
    twitter: {
      card: "summary_large_image",
      title: `${formattedType} Declaration Form`,
      description: `Fill out the ${formattedType} declaration form with our interactive assistant.`,
    },
  }
}

export default function Layout({ children }: { children: React.ReactNode }) {
  return <>{children}</>
}
