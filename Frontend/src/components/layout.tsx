import { cn } from "@/lib/utils"

import ClientForm from "./client-form"
import Footer from "./footer"
import Navbar from "./navbar"

type TLayout = {
  children: React.ReactNode
}

export default function Layout({ children }: TLayout) {
  return (
    <>
      <Navbar className={"font-sans"} />
      <main className={"min-h-screen font-sans antialiased"}>
        <div className="grid grid-cols-2">
          {children}
          <ClientForm />
        </div>
      </main>
      <Footer className={cn("font-sans")} />
    </>
  )
}
