"use client"

import { TStepMid } from "@/app/types/steps"

import { useFormMutation } from "@/lib/hooks/use-form-mutation"

import { Button } from "../ui/button"
import SaveStep from "./save-step"

export default function Step3({
  state,
  handleChange,
  handleNextStep,
  handlePreviousStep,
}: TStepMid) {
  const { formData } = state

  const { mutate, isPending } = useFormMutation()
  const handleSubmit = () => {
    mutate(state.nanoId)
  }
  return (
    <form className="flex flex-col gap-5">
      <div className="flex flex-col  gap-4 p-4">
        <h1 className="text-2xl font-bold">Opis czynności i kwota</h1>
        <p className="text-sm text-gray-500 mb-4">
          Pola wymagane zostały oznaczone gwiazdką (
          <span className="text-red-500">*</span>)
        </p>

        <div className="mb-4">
          <label
            htmlFor="action_description"
            className="block mb-2 font-semibold"
          >
            Opis czynności<span className="text-red-500">*</span>
          </label>
          <textarea
            id="action_description"
            name="action_description"
            className="w-full p-2 border rounded invalid:border-red-500 invalid:border-b"
            required
            value={formData.action_description}
            onChange={(e) => handleChange("action_description", e.target.value)}
            rows={4}
          />
          {!formData.action_description && (
            <p className="text-sm text-red-500 mt-1">Pole jest obowiązkowe</p>
          )}
        </div>

        <div className="mb-4">
          <label htmlFor="amount" className="block mb-2 font-semibold">
            Kwota<span className="text-red-500">*</span>
          </label>
          <input
            type="text"
            id="amount"
            name="amount"
            className="w-full p-2 border rounded invalid:border-red-500 invalid:border-b"
            required
            value={formData.amount}
            onChange={(e) => handleChange("amount", e.target.value)}
          />
          {!formData.amount && (
            <p className="text-sm text-red-500 mt-1">Pole jest obowiązkowe</p>
          )}
        </div>
        <p className="text-sm text-gray-500 mt-1">
          Wpisz kwotę w formacie 0.00
        </p>
      </div>

      <div className="flex gap-4 mt-4 bg-muted p-4 border-t border-input">
        <Button
          type="button"
          variant="outline"
          className="font-bold"
          onClick={handlePreviousStep}
        >
          Poprzedni krok
        </Button>
        <Button type="button" className="font-bold" onClick={handleNextStep}>
          Następny krok
        </Button>
        <SaveStep
          handleSubmit={handleSubmit}
          className="ml-auto"
          disabled={state.sameDataAsResponse === false || isPending}
        />
      </div>
    </form>
  )
}
