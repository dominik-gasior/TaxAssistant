'use client'
import { Button } from "@/components/ui/button"
import { useForm } from "@/lib/hooks/use-form"
import { ChevronLeft } from "lucide-react"
import { HandleChangeFunction } from "../types/steps"
import Step1 from "@/components/steps/1"
import Step2 from "@/components/steps/2"
import Step3 from "@/components/steps/3"
import Step4 from "@/components/steps/4"

export default function Page() {
  const { state, dispatch } = useForm()
  const { currentStep, totalSteps } = state

  
  const handleNextStep = () => dispatch({ type: "NEXT_STEP" })
  const handlePreviousStep = () => dispatch({ type: "PREVIOUS_STEP" })


  const handleChange: HandleChangeFunction = (name: string, value: any) => {
    dispatch({
      type: "UPDATE_FORM",
      payload: { name, value },
    })
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

      {currentStep === 1 && (
        <Step1
          state={state}
          handleChange={handleChange}
          handleNextStep={handleNextStep}
          onSave={handleSave}
        />
      )}
      {currentStep === 2 && (
        <Step2
          state={state}
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
