"use client"

import { TFormData, TStepFinal } from "@/app/types/steps"

import { Button } from "../ui/button"

export default function Step4({
  state,
  handlePreviousStep,
}: TStepFinal) {
  const { formData } = state
  const renderAddress = (address: TFormData["address"]) => {
    return (
      <>
        <p>
          {address.street} {address.house_number}
          {address.apartment_number ? `/${address.apartment_number}` : ""}
        </p>
        <p>
          {address.postal_code} {address.city}
        </p>
        <p>{address.country}</p>
      </>
    )
  }

  const renderTaxpayerData = (data: TFormData["taxpayer_data"]) => {
    if (data && "first_name" in data) {
      return (
        <>
          <p>
            Imię i nazwisko: {data.first_name} {data.last_name}
          </p>
          <p>PESEL: {data.pesel}</p>
          <p>Data urodzenia: {data.date_of_birth}</p>
        </>
      )
    } else {
      return (
        <>
          <p>Nazwa pełna: {data?.full_name}</p>
          <p>Nazwa skrócona: {data?.short_name}</p>
          <p>NIP: {data?.nip}</p>
        </>
      )
    }
  }

  return (
    <div className="flex flex-col gap-5 ">
      <div className="flex flex-col  gap-4 p-4">
        <h1 className="text-2xl font-bold">Podsumowanie</h1>
        <p className="text-sm text-gray-500 mb-4">
          Proszę sprawdzić poprawność wprowadzonych danych
        </p>

        <section className="border p-4 rounded-md">
          <h2 className="font-semibold mb-2">Krok 1: Informacje podstawowe</h2>
          <p>Data czynności: {formData.date_of_action}</p>
          <p>Urząd: {formData.office_name}</p>
          <p>Podmiot składający: {formData.entity_submitting_action}</p>
          <p>
            Typ podatnika:{" "}
            {formData.taxpayer_type === "individual"
              ? "Osoba fizyczna"
              : "Firma"}
          </p>
          {renderTaxpayerData(formData.taxpayer_data)}
        </section>

        <section className="border p-4 rounded-md">
          <h2 className="font-semibold mb-2">Krok 2: Adres</h2>
          {renderAddress(formData.address)}
        </section>

        <section className="border p-4 rounded-md">
          <h2 className="font-semibold mb-2">Krok 3: Opis czynności i kwota</h2>
          <p>Opis: {formData.action_description}</p>
          <p>Kwota: {formData.amount}</p>
        </section>
      </div>
      <div className="flex gap-4 mt-4 bg-muted p-4 border-t border-input">
        <Button
          type="button"
          variant='outline'
          className="font-bold"
          onClick={handlePreviousStep}
        >
          Poprzedni krok
        </Button>
        <Button
          type="submit"
          className="font-bold"
          onClick={() => {
            // Here you would typically submit the form data
            console.log("Form submitted:", formData)
          }}
        >
          Zatwierdź i wyślij
        </Button>
        
        <Button
          type="button"
          className="font-bold ml-auto"
          onClick={() => {
            const conversationId = state.nanoId; // Assuming nanoId is the conversationId
            if (conversationId) {
              window.open(`${process.env.NEXT_PUBLIC_BASE_URL}/download-file/${conversationId}`, '_blank');
            } else {
              console.error('Conversation ID is not available');
              // You might want to show an error message to the user here
            }
          }}
        >
          Pobierz plik
        </Button>

        
      </div>
    </div>
  )
}
