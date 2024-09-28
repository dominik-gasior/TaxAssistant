"use client"

import { TStepStart } from "@/app/types/steps"

import { Button } from "../ui/button"

export default function Step1({
  formData,
  handleChange,
  handleNextStep,
}: TStepStart) {
  return (
    <form className="flex flex-col gap-5">
      <div className="flex flex-col gap-4 p-4">
        <h1 className="text-2xl font-bold">
          Okres, miejsce i cel składania deklaracji
        </h1>
        <p className="text-sm text-gray-500 mb-4">
          Pola wymagane zostały oznaczone gwiazdką (
          <span className="text-red-500">*</span>)
        </p>

        <div className="mb-4">
          <label htmlFor="date" className="block mb-2 font-semibold">
            Data dokonania czynności<span className="text-red-500">*</span>
          </label>
          <input
            type="date"
            id="date"
            name="date"
            className="w-full p-2 border rounded invalid:border-red-500 invalid:border-b"
            required
            value={formData.date_of_action || ""}
            onChange={(e) => handleChange("date_of_action", e.target.value)}
          />
          {!formData.date_of_action && (
            <p className="text-sm text-red-500 mt-1">Pole jest obowiązkowe</p>
          )}
        </div>
        <p className="text-sm text-gray-500 mt-1">
          Wpisz datę w formacie DD.MM.RRRR
        </p>

        <div className="mb-4">
          <label htmlFor="office" className="block mb-2 font-semibold">
            Urząd, do którego jest adresowana deklaracja
            <span className="text-red-500">*</span>
          </label>
          <select
            id="office"
            name="office"
            className="w-full p-2 border rounded"
            required
            value={formData.office_name || ""}
            onChange={(e) => handleChange("office_name", e.target.value)}
          >
            <option value="">Wybierz lub wyszukaj</option>
            {/* Add more options here */}
          </select>
          {!formData.office_name && (
            <p className="text-sm text-red-500 mt-1">Pole jest obowiązkowe</p>
          )}
        </div>
      </div>
      <div className="flex gap-4 mt-4 bg-muted p-4 border-t border-input">
        <Button type="button" className="font-bold" onClick={handleNextStep}>
          Następny krok
        </Button>
      </div>
    </form>
  )
}
