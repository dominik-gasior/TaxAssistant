"use client"

import React, { createContext, Dispatch, useContext, useReducer } from "react"
import { TFormState } from "@/app/types/steps"

import { updateNestedObject } from "../utils"

const initialFormState: TFormState = {
  currentStep: 1,
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
  error: "",
  messages: [],
}

type Action =
  | { type: "LOGIN"; payload: { pesel: string } }
  | { type: "UPDATE_FORM"; payload: { name: string; value: any } }
  | { type: "SET_RESPONSE_DATA"; payload: any }
  | { type: "NEXT_STEP" }
  | { type: "PREVIOUS_STEP" }
  | { type: "SET_ERROR"; payload: string }
  | { type: "SET_MESSAGES"; payload: any }
  | { type: "RESET_FORM" }
const reducer = (state: TFormState, action: Action): TFormState => {
  switch (action.type) {
    case "RESET_FORM":
        console.log("invoike kurwa reset pizdo");
        
      localStorage.removeItem("newChatId") // AND delete the localstorage id

      return { ...initialFormState, messages: [] }
    case "SET_MESSAGES":
      return {
        ...state,
        messages: action.payload,
      }
    case "SET_ERROR":
      return {
        ...state,
        error: action.payload,
      }
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

type FormContextType = {
  state: TFormState
  dispatch: Dispatch<Action>
}

const FormContext = createContext<FormContextType | undefined>(undefined)

export const FormProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [state, dispatch] = useReducer(reducer, initialFormState)
  console.log(state)

  return (
    <FormContext.Provider value={{ state, dispatch }}>
      {children}
    </FormContext.Provider>
  )
}

export const useForm = () => {
  const context = useContext(FormContext)
  if (context === undefined) {
    throw new Error("useForm must be used within a FormProvider")
  }
  return context
}
