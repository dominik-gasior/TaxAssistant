import Footer from "./footer"
import Navbar from "./navbar"

type TLayout = {
  children: React.ReactNode
}

export default function Layout({ children }: TLayout) {

  return (
    <>
      <Navbar className="font-sans" />
      <main className="min-h-[100dvh_-_120.8px] max-h-[100dvh_-_120.8px] font-sans antialiased">
        <div className="grid grid-cols-2 p-6 gap-6">
          {children}
        </div>
      </main>
      <Footer className="font-sans" />
    </>
  )
}
