/* eslint-disable @typescript-eslint/no-explicit-any */
"use client"

import { useEffect, useState } from "react"
import { useRouter } from "next/navigation"
import { useQuery } from "@tanstack/react-query"
import { ChevronLeft } from "lucide-react"
import { nanoid } from "nanoid"

import { restoreChat } from "@/lib/action"
import { useForm } from "@/lib/hooks/use-form"
import { useLocalStorage } from "@/lib/hooks/use-local-storage"
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
import ClientForm from "@/components/client-form"
import Step1 from "@/components/steps/1"
import Step2 from "@/components/steps/2"
import Step3 from "@/components/steps/3"
import Step4 from "@/components/steps/4"

import { HandleChangeFunction, TFormMessage } from "../types/steps"

export default function Page({ params }: { params: { type: string } }) {
  const { state, dispatch } = useForm()
  const { currentStep, totalSteps } = state
  const [storedNanoId, setStoredNanoId] = useLocalStorage("newChatId", nanoid())
  const [isInitialLoad, setIsInitialLoad] = useState(true)
  const [queryEnabled, setQueryEnabled] = useState(false)

  useEffect(() => {
    if (!state.nanoId) {
      const userAgreed = window.confirm("Czy chcesz użyć poprzedniego czatu?")
      if (userAgreed) {
        dispatch({ type: "SET_NANO_ID", payload: storedNanoId })
        setQueryEnabled(true)
      } else {
        const newNanoId = nanoid()
        setStoredNanoId(newNanoId)
        dispatch({ type: "SET_NANO_ID", payload: newNanoId })
      }
    }
  }, [])

  const { data: restoredChat, status } = useQuery({
    queryKey: ["restoreChat", storedNanoId],
    queryFn: () => restoreChat(storedNanoId),
    refetchOnWindowFocus: false,
    retry: false,
    refetchOnMount: false,
    enabled: queryEnabled,
    staleTime: Infinity, // Prevent auto-refetching
    gcTime: Infinity, // Keep the data cached indefinitely
  })

  useEffect(() => {
    if (isInitialLoad && restoredChat && status === "success") {
      dispatch({ type: "SET_RESPONSE_DATA", payload: restoredChat.formData })

      // Update messages state with the chat log
      const messages = restoredChat.messages.map((message: TFormMessage) => ({
        content: message.content,
        role: message.role,
        timestamp: new Date(Number(message.timestamp) * 1000).toISOString(),
      }))
      dispatch({ type: "SET_MESSAGES", payload: messages })

      setIsInitialLoad(false)
    }
  }, [restoredChat, dispatch, status, isInitialLoad])

  const handleNextStep = () => dispatch({ type: "NEXT_STEP" })
  const handlePreviousStep = () => dispatch({ type: "PREVIOUS_STEP" })

  const handleChange: HandleChangeFunction = (name: string, value: any) => {
    dispatch({
      type: "UPDATE_FORM",
      payload: { name, value },
    })
  }
  const { back } = useRouter()

  return (
    <>
      <div className="max-w-2xl mx-auto  w-full overflow-auto border-r border-input">
        <div className="flex gap-4 items-center py-2 text-lg px-4">
          <Button onClick={back}>
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
      <ClientForm declarationType={params.type} id={storedNanoId} />
    </>
  )
}
