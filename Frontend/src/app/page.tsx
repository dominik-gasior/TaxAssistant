// Remove the 'use client' directive
'use client'
import { useReducer } from 'react'
type TFormState = {
  step: number
  formData: {
   
  }
}
const initialFormState: TFormState = {
  step: 1,
  formData: {
    name: '',
    email: '',
    message: '',
  },
}

const reducer = (state: any, action: any) => {
  switch (action.type) {
    case 'NEXT_STEP':
      return { ...state, step: state.step + 1 }
    default:
      return state
  }
}

export default function Page() {
  const [form, dispatch] = useReducer(reducer, initialFormState)

  // handle nexdt step
  const handleNextStep = () => {
    dispatch({ type: 'NEXT_STEP' })
  }

  return (
    <form>
      
    </form>
  )
}