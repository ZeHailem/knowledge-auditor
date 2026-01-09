# Knowledge Auditor API (Responsible AI Governance) 🚀

> **Status:** Active development (WIP) — core governance flow works end-to-end.

## What this is
**Knowledge Auditor API** is an ASP.NET Core service that audits a user request against **company policy knowledge** and returns an **explainable, evidence-backed decision**.

Instead of building “just QA,” this project focuses on **governance and enforcement**:
- Is the response grounded in approved company knowledge?
- Which policy section supports the decision?
- Should the request be **Approved**, **Rejected**, or **Escalated**?

## Why this matters (Responsible AI)
In enterprise environments, the problem is rarely “can the model answer?”
It’s:
- Can we **trust** the answer?
- Can we **audit** it later?
- Can we **enforce** business rules and reduce risk?

This project demonstrates a practical pattern:
> **Retrieve evidence → Evaluate confidence → Enforce a policy decision → Return explainable output**

## Key features
- **Evidence-first design**: decisions are backed by policy sections (document + page + section ID)
- **Governance pipeline**: clean separation of retrieval, evaluation, and enforcement
- **Deterministic behavior** (no LLM required for Phase 1)
- **Swagger/OpenAPI** docs for easy exploration
- Unit tests for core components (retrieval / evaluation / decision logic)

---

## Architecture (high level)

**Flow:**
1. **Knowledge Store** loads HR policies from `hr-policies.json`
2. **Retrieval Service** finds relevant policy sections for a question
3. **Evaluation Service** scores confidence based on evidence
4. **Policy Decision Engine** enforces outcome rules
5. **Governance Pipeline** orchestrates everything and outputs `AuditDecision`

**Components:**
- `InMemoryHrKnowledgeStore` → loads company policy knowledge
- `HrRetrievalService` → retrieves relevant policy sections
- `SimpleEvaluationService` → produces `EvaluationResult` (confidence, evidence count)
- `PolicyDecisionEngine` → returns `AuditDecision` (Approved/Rejected/Escalated)
- `GovernancePipeline` → orchestrator

---

## API

### POST `/audit`
Audits a question against company HR policies and returns a decision + evidence.

**Request**
```json
{
  "question": "Termination decisions must be documented"
}
```


**Response**

```json
{
  "outcome": "Escalated",
  "riskLevel": "Medium",
  "reason": "Supporting policy found, but confidence is below approval threshold.",
  "evidence": [
    {
      "policyId": "HR-POL-12",
      "document": "Employee Termination Policy",
      "sectionId": "4.3",
      "pageNumber": 19
    }
  ],
  "auditId": "f3382f1f-3d2c-42e2-b62c-4ccf4876b47c"
}
