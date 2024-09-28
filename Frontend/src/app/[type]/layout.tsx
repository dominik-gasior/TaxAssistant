import ClientForm from "@/components/client-form"

export default function Layout({
  children,
  params,
}: {
  children: React.ReactNode
  params: { type: string }
}) {
  return (
    <>
      {children}
      <ClientForm declarationType={params.type} />
    </>
  )
}
