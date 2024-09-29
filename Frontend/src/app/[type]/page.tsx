"use client"

import { useMutation } from "@tanstack/react-query"
import { ChevronLeft } from "lucide-react"

import { useForm } from "@/lib/hooks/use-form"
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger,
} from "@/components/ui/alert-dialog"
import { Button } from "@/components/ui/button"
import Step1 from "@/components/steps/1"
import Step2 from "@/components/steps/2"
import Step3 from "@/components/steps/3"
import Step4 from "@/components/steps/4"

import { HandleChangeFunction } from "../types/steps"

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

  return (
    <div className="max-w-2xl mx-auto  w-full overflow-auto border-r border-input">
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

        <AlertDialog>
          <AlertDialogTrigger asChild>
            <Button
              variant="destructive"
              className="ml-auto"
              onClick={() => dispatch({ type: "RESET_FORM" })}
            >
              Reset
            </Button>
          </AlertDialogTrigger>
          <AlertDialogContent>
            <AlertDialogHeader>
              <AlertDialogTitle>
                Czy na pewno chcesz to zrobić?
              </AlertDialogTitle>
              <AlertDialogDescription>
                Ta akcja jest nieodwracalna. Spowoduje to usunięcie wszystkich
                informacji, historię czatu oraz dane we wszystkich rubrykach
                formularza.
              </AlertDialogDescription>
            </AlertDialogHeader>
            <AlertDialogFooter>
              <AlertDialogCancel>Anuluj</AlertDialogCancel>
              <AlertDialogAction
                onClick={() => dispatch({ type: "RESET_FORM" })}
              >
                Kontynuuj
              </AlertDialogAction>
            </AlertDialogFooter>
          </AlertDialogContent>
        </AlertDialog>
      </div>

      {currentStep === 1 && (
        <Step1
          state={state}
          handleChange={handleChange}
          handleNextStep={handleNextStep}
        />
      )}
      {currentStep === 2 && (
        <Step2
          state={state}
          handleChange={handleChange}
          handleNextStep={handleNextStep}
          handlePreviousStep={handlePreviousStep}
        />
      )}
      {currentStep === 3 && (
        <Step3
          state={state}
          handleChange={handleChange}
          handleNextStep={handleNextStep}
          handlePreviousStep={handlePreviousStep}
        />
      )}
      {currentStep === 4 && (
        <Step4
          state={state}
          handleChange={handleChange}
          handlePreviousStep={handlePreviousStep}
        />
      )}
    </div>
  )
}
