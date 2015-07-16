module Translator

open Microsoft.FSharp.Quotations
open System.Linq.Expressions
open System
open Microsoft.FSharp.Quotations.Patterns

/// <summary>
/// Converts quotation with lambda property getter into
/// MemberAccessExpression wrapped into LambdaExpression
/// </summary>
/// <param name="expr"></param>
let getProperty (expr: Expr<'a -> 'b>): Expression<Func<'a, 'b>> =
    match expr with
    | Lambda(_, body) ->
        match body with
        | PropertyGet(_, pinfo, _) ->
            let arg = Expression.Parameter(typeof<'a>)
            let getter = Expression.Property(arg, pinfo) :> Expression
            Expression.Lambda<Func<'a, 'b>>(getter, arg)
        | _ -> failwith "getProperty translator accepts only quotations with property getters"
    | _ -> failwith "Quotation does not contain lambda function"