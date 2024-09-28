import { clsx, type ClassValue } from "clsx"
import { twMerge } from "tailwind-merge"

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}

export const updateNestedObject = (obj: any, path: string, value: any) => {
  const keys = path.split(".")
  const lastKey = keys.pop()!
  const lastObj = keys.reduce((obj, key) => (obj[key] = obj[key] || {}), obj)
  lastObj[lastKey] = value
  return { ...obj }
}