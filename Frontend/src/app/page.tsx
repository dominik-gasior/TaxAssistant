"use client"

import Link from "next/link"
import { ChevronLeft, ChevronRight } from "lucide-react"

import { useForm } from "@/lib/hooks/use-form"
import { Button } from "@/components/ui/button"

const TAX_TYPES = [
  {
    name: "PIT",
    link: "pit",
    description:
      "Tu rozliczysz podatek dochodowy od osób fizycznych. Informacje o ulgach, odliczeniach, stawkach i limitach.",
  },
  {
    name: "CIT",
    link: "cit",
    description:
      "Tu rozliczysz podatek dochodowy od osób prawnych. Informacje o ulgach, stawkach i limitach.",
  },
  {
    name: "VAT",
    link: "vat",
    description:
      "Tu rozliczysz podatek od wartości dodanej. Informacje o ulgach, stawkach i limitach.",
  },
  {
    name: "Akcyza",
    link: "akcyza",
    description:
      "Podatek od niektórych rodzajów wyrobów. Informacje o stawkach, akcyzach, EMCS.",
  },
  {
    name: "Cło",
    link: "clo",
    description:
      "Informacje dla osób fizycznych, przedsiębiorców, taryfy celne, ograniczenia przywozu i wywozu towaru, zwolnienia.",
  },
  {
    name: "PCC3",
    link: "pcc3",
    description:
      "Tu rozliczysz podatek od czynności cywilnoprawnych oraz podatków od spadków i darowizn. Ulgi, podliczenia, stawki i limity.",
  },
  {
    name: "Podatki i opłaty lokalne",
    link: "podatki-i-oplaty-lokalne",
    description:
      "Podatki od: nieruchomości, środków transportowych, rolnych, leśnych, gminy, opłaty targowa, miejscowa, uzdrowiskowa, reklamowa, stawki i zwolnienia.",
  },
  {
    name: "Inne podatki",
    link: "inne-podatki",
    description:
      "Podatek od gier hazardowych, tonarzy, od wyrobów niektórych, instytucji publicznych, opłata skarbowa.",
  },
]

// Helper function to update nested objects

export default function Page() {

  return (
    <div className=" w-full col-span-full">
     

      <div className="flex flex-col gap-4 p-4">
        <h1 className="text-2xl font-bold">Wybierz rodzaj deklaracji</h1>
        <div className="grid grid-cols-4 gap-4">
          {TAX_TYPES.map((tax) => (
            <Link
              className="bg-muted rounded-md w-full flex items-center justify-center  p-6"
              key={tax.link}
              href={`/${tax.link}`}
            >
              <div className="flex flex-col items-center gap-2">
                <div className="flex  text-xl font-boldflex-row w-full items-center justify-between">
                  <p className="font-bold">{tax.name}</p>
                  <ChevronRight size={24} />
                </div>
                <span className="text-sm">{tax.description}</span>
              </div>
            </Link>
          ))}
        </div>
      </div>
    </div>
  )
}
