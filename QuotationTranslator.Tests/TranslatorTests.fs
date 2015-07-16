module TranslatorTests

open Translator
open Xunit

type TestEntity(id) =
    member val Id = id with get, set

[<Fact>]
let SimpleGetterTest() =
    let expr = getProperty <@ fun (x: TestEntity) -> x.Id @>
    ()