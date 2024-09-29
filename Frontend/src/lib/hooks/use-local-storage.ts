/* eslint-disable @typescript-eslint/no-unused-vars */
import { useEffect, useState } from 'react'

export const useLocalStorage = <T>(
  key: string,
  initialValue: T
): [T, (value: T) => void] => {
  const [storedValue, setStoredValue] = useState(initialValue)
  const [isInitialLoad, _] = useState(true)
  // // On initial load, check if a key called 'newChatId' exists in localStorage. If it does, prompt the user to decide whether to continue from the last chat history or to clear it.


  // useEffect(() => {
  //   if (isInitialLoad) {
  //     const item = window.localStorage.getItem(key)
  //     if (item !== null) {
  //       setStoredValue(JSON.parse(item))
  //     }
  //     setIsInitialLoad(false)
  //   }
  // }, [isInitialLoad, key])



  useEffect(() => {
    // Retrieve from localStorage
    if (isInitialLoad) {

      const item = window.localStorage.getItem(key)
      if (item !== null) {
        const userAgreed = window.confirm('Do you agree to load the existing data from local storage?')
        if (userAgreed) {
          setStoredValue(JSON.parse(item))
        }
      }
    }

  }, [key, isInitialLoad])

  const setValue = (value: T) => {
    // Save state
    setStoredValue(value)
    // Save to localStorage
    window.localStorage.setItem(key, JSON.stringify(value))
  }
  return [storedValue, setValue]
}
