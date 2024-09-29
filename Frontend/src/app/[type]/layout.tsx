import ClientForm from "@/components/client-form"
import { nanoid } from "nanoid"

export default function Layout({
  children,
  params,
}: {
  children: React.ReactNode
  params: { type: string }
}) {
  const nanoId = nanoid ()
  return (
    <>
      {children}
      <ClientForm declarationType={params.type} id={nanoId} />
    </>
  )
}
