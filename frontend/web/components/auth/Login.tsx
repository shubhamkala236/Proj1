// "use client"

// import { zodResolver } from "@hookform/resolvers/zod"
// import { useForm } from "react-hook-form"
// import * as z from "zod"
// import { useState } from "react"

// import { Button } from "@/components/ui/button"
// import { Input } from "@/components/ui/input"
// import { Label } from "@/components/ui/label"
// import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
// import { Field, FieldContent, FieldLabel } from "@/components/ui/field"

// const formSchema = z.object({
//   email: z.string().email("Please enter a valid email address"),
//   password: z.string().min(6, "Password must be at least 6 characters long"),
// })

// type FormValues = z.infer<typeof formSchema>

// const Login = () => {
//   const [isLoading, setIsLoading] = useState(false)
//   const [error, setError] = useState<string | null>(null)

//   const { register, handleSubmit, formState: { errors } } = useForm<FormValues>({
//     resolver: zodResolver(formSchema),
//   })

//   const onSubmit = async (data: FormValues) => {
//     setIsLoading(true)
//     setError(null)
//     try {
//       // Add your login logic here
//       console.log("Form submitted:", data)
//     } catch (err) {
//       setError("Login failed. Please try again.")
//     } finally {
//       setIsLoading(false)
//     }
//   }

//   return (
//     <div className="flex items-center justify-center min-h-screen bg-background">
//       <Card className="w-full max-w-md">
//         <CardHeader>
//           <CardTitle>Login</CardTitle>
//           <CardDescription>Enter your credentials to sign in</CardDescription>
//         </CardHeader>
//         <CardContent>
//           <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
//             {error && (
//               <div className="bg-destructive/10 text-destructive text-sm p-3 rounded-md">
//                 {error}
//               </div>
//             )}

//             <Field>
//               <FieldLabel htmlFor="email">Email</FieldLabel>
//               <FieldContent>
//                 <Input
//                   id="email"
//                   type="email"
//                   placeholder="you@example.com"
//                   {...register("email")}
//                   aria-invalid={!!errors.email}
//                 />
//                 {errors.email && (
//                   <p className="text-destructive text-sm">{errors.email.message}</p>
//                 )}
//               </FieldContent>
//             </Field>

//             <Field>
//               <FieldLabel htmlFor="password">Password</FieldLabel>
//               <FieldContent>
//                 <Input
//                   id="password"
//                   type="password"
//                   placeholder="Enter your password"
//                   {...register("password")}
//                   aria-invalid={!!errors.password}
//                 />
//                 {errors.password && (
//                   <p className="text-destructive text-sm">{errors.password.message}</p>
//                 )}
//               </FieldContent>
//             </Field>

//             <Button type="submit" className="w-full" disabled={isLoading}>
//               {isLoading ? "Signing in..." : "Sign In"}
//             </Button>
//           </form>

//           <p className="text-center text-sm text-muted-foreground mt-4">
//             Don't have an account?{" "}
//             <a href="/signup" className="text-primary hover:underline">
//               Sign up
//             </a>
//           </p>
//         </CardContent>
//       </Card>
//     </div>
//   )
// }

// export default Login