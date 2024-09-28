"use client"

import { useReducer, useState } from "react"
import { ChevronLeft } from "lucide-react"

import { Button } from "@/components/ui/button"
import Step1 from "@/components/steps/1"
import Step2 from "@/components/steps/2"
import Step3 from "@/components/steps/3"
import Step4 from "@/components/steps/4"
import SaveStep from "@/components/steps/save-step"

import { HandleChangeFunction, TFormState } from "./types/steps"

const initialFormState: TFormState = {
  currentStep: 0,
  totalSteps: 4,
  sameDataAsResponse: false,
  formData: {
    date_of_action: "",
    office_name: "",
    entity_submitting_action: 1,
    taxpayer_type: "individual",
    taxpayer_data: {
      first_name: "",
      last_name: "",
      pesel: "",
      date_of_birth: "",
    },
    address: {
      country: "",
      province: "",
      county: "",
      municipality: "",
      street: "",
      house_number: "",
      apartment_number: "",
      city: "",
      postal_code: "",
    },
    action_description: "",
    amount: 0,
  },
}

const reducer = (state: TFormState, action: any) => {
  switch (action.type) {
    // if the user logged in thenw e update pesel from an action payload, name last name and birthdate with some hardcoded data

    case "LOGIN":
      return {
        ...state,
        formData: {
          ...state.formData,
          taxpayer_data: {
            ...state.formData.taxpayer_data,
            pesel: action.payload.pesel,
            first_name: "Jan",
            last_name: "Kowalski",
            date_of_birth: "1990-01-01",
          },
        },
      }
    case "UPDATE_FORM":
      return {
        ...state,
        formData: updateNestedObject(
          state.formData,
          action.payload.name,
          action.payload.value
        ),
        sameDataAsResponse: true,
      }
    case "SET_RESPONSE_DATA":
      return {
        ...state,
        formData: action.payload,
        sameDataAsResponse: false,
      }
    case "NEXT_STEP":
      return { ...state, currentStep: state.currentStep + 1 }
    case "PREVIOUS_STEP":
      return { ...state, currentStep: state.currentStep - 1 }
    default:
      return state
  }
}

// Helper function to update nested objects
const updateNestedObject = (obj: any, path: string, value: any) => {
  const keys = path.split(".")
  const lastKey = keys.pop()!
  const lastObj = keys.reduce((obj, key) => (obj[key] = obj[key] || {}), obj)
  lastObj[lastKey] = value
  return { ...obj }
}

export default function Page() {
  const [state, dispatch] = useReducer(reducer, initialFormState)
  console.log(state.formData)

  const [_, setTaxType] = useState<string | null>(null)
  const [error, setError] = useState<string | null>(null)

  const handleNextStep = () => {
    dispatch({ type: "NEXT_STEP" })
  }

  const handlePreviousStep = () => {
    dispatch({ type: "PREVIOUS_STEP" })
  }

  const handleChange: HandleChangeFunction = (name: string, value: any) => {
    dispatch({
      type: "UPDATE_FORM",
      payload: { name, value },
    })
  }
  const handleTaxSelection = (selectedTax: string) => {
    if (selectedTax === "PCC") {
      setTaxType(selectedTax)
      setError(null)
      handleNextStep()
    } else {
      setError("Przepraszamy, ten rodzaj podatku nie jest jeszcze obsługiwany.")
    }
  }

  const handleSave = async () => {
    try {
      const response = await fetch("/api/save-data", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(state.formData),
      })

      if (!response.ok) {
        throw new Error("Failed to save data")
      }

      const responseData = await response.json()
      dispatch({ type: "SET_RESPONSE_DATA", payload: responseData })
    } catch (error) {
      console.error("Error saving data:", error)
      // Handle error (e.g., show error message to user)
    }
  }

  const { currentStep, totalSteps, formData } = state

  return (
    <div className="max-w-2xl mx-auto  w-full  border-r border-input">
      {currentStep !== 0 && (
        <div className="flex gap-4 items-center py-2 text-lg ">
          <Button onClick={handlePreviousStep}>
            <ChevronLeft size={16} /> Wroc
          </Button>

          <div className="bg-muted rounded-md w-fit px-4 h-10 flex items-center">
            <h3>
              Krok{" "}
              <span className="text-foreground font-bold">{currentStep}</span> /{" "}
              {totalSteps}
            </h3>
          </div>
        </div>
      )}

      {currentStep === 0 && (
        <div className="flex flex-col gap-4 p-4">
          <h1 className="text-2xl font-bold">Wybierz podatek</h1>
          <div className="grid grid-cols-2 gap-4">
            {[
              "PIT",
              "CIT",
              "VAT",
              "Akcyza",
              "Cło",
              "PCC",
              "Podatki i opłaty lokalne",
              "Inne podatki",
            ].map((tax) => (
              <Button key={tax} onClick={() => handleTaxSelection(tax)}>
                {tax}
              </Button>
            ))}
          </div>
          {error && <p className="text-red-500 mt-4">{error}</p>}
        </div>
      )}

      {currentStep === 1 && (
        <Step1
          formData={formData}
          handleChange={handleChange}
          handleNextStep={handleNextStep}
          onSave={handleSave}
        />
      )}
      {currentStep === 2 && (
        <Step2
          formData={formData}
          handleChange={handleChange}
          handleNextStep={handleNextStep}
          handlePreviousStep={handlePreviousStep}
          onSave={handleSave}
        />
      )}
      {currentStep === 3 && (
          <Step3
            state={state}
            handleChange={handleChange}
            handleNextStep={handleNextStep}
            handlePreviousStep={handlePreviousStep}
            onSave={handleSave}
          />
      )}
      {currentStep === 4 && (
          <Step4
            state={state}
            handleChange={handleChange}
            handlePreviousStep={handlePreviousStep}
            onSave={handleSave}
          />
      )}
    </div>
  )
}
