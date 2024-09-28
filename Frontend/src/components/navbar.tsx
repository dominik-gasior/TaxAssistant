import React from "react"
import Link from "next/link"

import { cn } from "@/lib/utils"

type Props = {
  className?: string
}

function Navbar({ className }: Props) {
  return (
    <header className={cn("border-b border-muted-foreground w-full", className)}>
      <nav className="h-[4.5rem] flex items-center px-6 text-center ">
        <div>
          <Link href="/" className="flex items-center gap-3">
            <img width="28" src="	https://klient-eformularz.mf.gov.pl/declaration/assets/img/polish-coa.svg" alt="Herb Polski" />
            <span className="text-2xl font-bold font-montserrat">podatki.gov.pl</span>
            <span className="sr-only">(link otwiera nowe okno)</span>
          </Link>
        </div>
      </nav>
    </header>
  )
}

export default Navbar
